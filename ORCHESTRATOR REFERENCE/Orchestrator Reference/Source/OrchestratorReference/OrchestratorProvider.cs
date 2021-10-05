namespace C60.OrchestratorReference
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services.Client;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using C60.OrchestratorReference.OrchestratorServiceReference;

    /// <summary>
    /// Query the Orchestrator web services
    /// </summary>
    public class OrchestratorProvider
    {
        private const int DefaultDelayBetweenResultCheckSeconds = 5;
        private const int DefaultTimeoutSeconds = 0;

        private Uri serviceUri;
        private OrchestratorContext orchestratorContext;

        public OrchestratorProvider(Uri orchestratorWebServerUri)
        {
            this.serviceUri = orchestratorWebServerUri;
            this.SetUpOrchestratorProvider();
        }

        public OrchestratorProvider(string orchestratorWebServer, ICredentials credentials) : this(false, orchestratorWebServer, 81, credentials)
        {
        }

        public OrchestratorProvider(bool useSSL, string orchestratorWebServer, int port, ICredentials credentials) : this(useSSL, orchestratorWebServer, port)
        {
            if (credentials != null)
            {
                this.orchestratorContext.Credentials = credentials;
            }
        }

        public OrchestratorProvider(bool useSSL, string orchestratorWebServer, int port)
        {
            if (string.IsNullOrEmpty(orchestratorWebServer))
            {
                throw new ArgumentNullException("orchestratorWebServer");
            }

            if (port <= 0)
            {
                port = 81;
            }

            this.serviceUri = new Uri(string.Format("http{0}://{1}:{2}/Orchestrator2012/Orchestrator.svc", useSSL ? "s" : string.Empty, orchestratorWebServer, port));

            this.SetUpOrchestratorProvider();
        }

        public OrchestratorContext OrchestratorContext
        {
            get
            {
                return this.orchestratorContext;
            }
        }

        public Dictionary<string, string> ReplaceFolderPrefix { get;  private set; }

        public OrchestratorContext CreateOrchestratorContext()
        {
            return new OrchestratorContext(this.serviceUri);
        }
        
        public List<Folder> GetFoldersAndRunbooks()
        {
            return DataServiceGetAllPages(this.orchestratorContext, this.orchestratorContext.Folders.Expand("Runbooks"));
        }

        public List<RunbookParameter> GetAllInParameters()
        {
            return DataServiceGetAllPages(this.orchestratorContext, this.orchestratorContext.RunbookParameters).Where(rp => rp.Direction == "In").ToList();
        }

        public Guid ExecuteRunbook(string runbookFullName, Dictionary<string, object> parameters)
        {
            Job job = new Job();
            try
            {
                runbookFullName = this.DoReplaceFolderPrefix(runbookFullName);
                Guid runbookId = this.GetRunbookId(runbookFullName);
                job.RunbookId = runbookId;
                job.Parameters = this.BuildParametersString(runbookId, parameters);
                this.orchestratorContext.AddToJobs(job);
                this.orchestratorContext.SaveChanges();
                return job.Id;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(string.Format("Error execution runbook \"{0}\" with parameter string: \"{1}\".{2}",
                                        runbookFullName, 
                                        job.Parameters, 
                                        ex.InnerException != null ? ex.InnerException.Message : string.Empty), ex);
            }
        }

        public Job GetJob(Guid jobId)
        {
            this.orchestratorContext.MergeOption = System.Data.Services.Client.MergeOption.OverwriteChanges;
            return this.orchestratorContext.Jobs.Where(j => j.Id == jobId).ToList().Single();
        }

        public Dictionary<string, string> GetJobResult(Guid jobId)
        {
            return GetJobResult(jobId, DefaultDelayBetweenResultCheckSeconds, DefaultTimeoutSeconds, CancellationToken.None);
        }

        public Dictionary<string, string> GetJobResult(Guid jobId, int delayBetweenResultCheckSeconds, int timeoutSeconds, CancellationToken cancellationToken)
        {
            if (jobId == null || jobId == Guid.Empty)
            {
                throw new ArgumentNullException("jobId");
            }

            Job job = this.GetJob(jobId);
            int retryTimes = 0;
            
            while ((job.Status == "Pending" || job.Status == "Running") && (timeoutSeconds <= 0 || retryTimes * delayBetweenResultCheckSeconds < timeoutSeconds))
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    job.Status = "Canceled";
                    this.orchestratorContext.UpdateObject(job);
                    this.orchestratorContext.SaveChanges();
                    return null;
                }

                Thread.Sleep(1000 * delayBetweenResultCheckSeconds);
                retryTimes++;
                job = this.GetJob(jobId);
            }

            if (job.Status != "Completed" && retryTimes * delayBetweenResultCheckSeconds >= timeoutSeconds)
            {
                throw new TimeoutException(string.Format("Job didn't complete after {0} seconds", timeoutSeconds));
            }

            if (job.Status != "Completed")
            {
                throw new Exception("Job execution " + job.Status.ToLower());
            }

            RunbookInstance runbookInstance = this.orchestratorContext.RunbookInstances.Where(ri => ri.JobId == jobId).ToList().Single();
            return this.orchestratorContext.RunbookInstanceParameters.Where(rip => rip.RunbookInstanceId == runbookInstance.Id && rip.Direction == "Out").ToDictionary(rip => rip.Name, rip => rip.Value);
        }

        public Task<Dictionary<string, string>> GetJobResultAsync(Guid jobId)
        {
            return GetJobResultAsync(jobId, DefaultDelayBetweenResultCheckSeconds, DefaultTimeoutSeconds, CancellationToken.None);
        }

        public Task<Dictionary<string, string>> GetJobResultAsync(Guid jobId, CancellationToken cancellationToken)
        {
            return GetJobResultAsync(jobId, DefaultDelayBetweenResultCheckSeconds, DefaultTimeoutSeconds, cancellationToken);
        }

        public Task<Dictionary<string, string>> GetJobResultAsync(Guid jobId, int delayBetweenResultCheckSeconds, int timeoutSeconds, CancellationToken cancellationToken)
        {
            return Task<Dictionary<string, string>>.Factory.StartNew(() => this.GetJobResult(jobId, delayBetweenResultCheckSeconds, timeoutSeconds, cancellationToken), cancellationToken);
        }

        private static List<T> DataServiceGetAllPages<T>(DataServiceContext context, DataServiceQuery<T> query)
        {
            List<T> result = new List<T>();
            
            QueryOperationResponse<T> response = query.Execute() as QueryOperationResponse<T>;
            DataServiceQueryContinuation<T> token = null;
            do
            {
                if (token != null)
                {
                    response = context.Execute<T>(token);
                }

                result.AddRange(response);
                token = response.GetContinuation();
            } while (token != null);

            return result;
        }

        private string DoReplaceFolderPrefix(string runbookPath)
        {
            string resultPath = runbookPath;
            foreach (var replacePrefix in this.ReplaceFolderPrefix)
            {
                if (runbookPath.StartsWith(replacePrefix.Key))
                {
                    resultPath = replacePrefix.Value + resultPath.Substring(replacePrefix.Key.Length);
                }
            }

            return resultPath;
        }

        private void SetUpOrchestratorProvider()
        {
            this.orchestratorContext = this.CreateOrchestratorContext();
            this.orchestratorContext.Credentials = CredentialCache.DefaultNetworkCredentials;
            this.ReplaceFolderPrefix = new Dictionary<string, string>();
        }

        private string BuildParametersString(Guid runbookId, Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return string.Empty;
            }

            Dictionary<string, string> runbookParameters = this.orchestratorContext.RunbookParameters.Where(rp => rp.RunbookId == runbookId && rp.Direction == "In")
                                                                .ToDictionary(rp => rp.Name, rp => rp.Id.ToString("B"));
            return new XElement(
                                "Data", 
                                parameters
                                .Where(p => p.Value != null)
                                .Select(
                                            p => new XElement(
                                                                "Parameter", 
                                                                new XElement("ID", runbookParameters[p.Key]), 
                                                                new XElement("Value", p.Value.ToString())))).ToString(SaveOptions.DisableFormatting);
        }

        private Guid GetRunbookId(string runbookFullName)
        {
            try
            {
                return this.orchestratorContext.Runbooks.Where(r => r.Path == runbookFullName).ToList().Single().Id;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(string.Format("Can't find the runbook \"{0}\". {1}", runbookFullName, ex.InnerException != null ? ex.InnerException.Message : string.Empty), ex);
            }
        }
    }
}
