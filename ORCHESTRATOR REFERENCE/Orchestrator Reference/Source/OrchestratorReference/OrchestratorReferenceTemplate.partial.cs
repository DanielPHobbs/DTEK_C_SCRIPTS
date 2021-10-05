namespace C60.OrchestratorReference
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using C60.OrchestratorReference.OrchestratorServiceReference;

    public partial class OrchestratorReferenceTemplate
    {
        private OrchestratorProvider orchestratorProvider;
        
        public bool UseSSL { get; set; }
        
        public string OrchestratorWebServer { get; set; }
        
        public int Port { get; set; }
        
        public ICredentials Credentials { get; set; }
        
        public string IncludePath { get; set; }
        
        public string ExcludePath { get; set; }
        
        public string Namespace { get; set; }
        
        public string ClassName { get; set; }

        private OrchestratorProvider OrchestratorProvider
        {
            get
            {
                if (this.orchestratorProvider == null)
                {
                    this.orchestratorProvider = new OrchestratorProvider(this.UseSSL, this.OrchestratorWebServer, this.Port, this.Credentials);
                }

                return this.orchestratorProvider;
            }
        }

        private void WriteFolderClass()
        {
            List<Folder> allFolder = this.OrchestratorProvider.GetFoldersAndRunbooks();
            if (!string.IsNullOrEmpty(this.IncludePath))
            {
                string[] includePaths = this.IncludePath.Split(',');
                allFolder = includePaths.SelectMany(ip => allFolder.Where(f => ip.Length > f.Path.Length ? ip.IndexOf(f.Path) != -1 : f.Path.StartsWith(ip))).GroupBy(f => f.Path).Select(g => g.First()).ToList();
            }

            if (!string.IsNullOrEmpty(this.ExcludePath))
            {
                string[] excludePaths = this.ExcludePath.Split(',');
                allFolder = excludePaths.SelectMany(ep => allFolder.Where(f => ep.Length > f.Path.Length || !f.Path.StartsWith(ep))).GroupBy(f => f.Path).Select(g => g.First()).ToList();
            }

            allFolder = allFolder.Where(f => f.Runbooks.Count > 0 || allFolder.Any(af => af.ParentId == f.Id)).ToList();
            List<RunbookParameter> allParameters = this.OrchestratorProvider.GetAllInParameters();
            Folder rootFolder = allFolder.Single(r => r.ParentId == null);
            foreach (Folder subFolder in allFolder.Where(f => f.ParentId == rootFolder.Id))
            {
                FolderTemplate t = new FolderTemplate();
                t.OrchestratorProvider = this.OrchestratorProvider;
                t.AllFolders = allFolder.ToList();
                t.CurrentFolder = subFolder;
                t.AllParameters = allParameters;
                this.Write(t.TransformText() + "\r\n");
            }
        }
    }
}
