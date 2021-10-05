namespace C60.OrchestratorReference
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using C60.OrchestratorReference.OrchestratorServiceReference;

    public partial class FolderTemplate
    {
        private IdentifierCleaner identifierCleaner = new IdentifierCleaner();
        
        private List<Runbook> runbooks;

        private IEnumerable<Folder> subfolders;
        
        public OrchestratorProvider OrchestratorProvider { get; set; }
        
        public List<Folder> AllFolders { get; set; }
        
        public Folder CurrentFolder { get; set; }
        
        public List<RunbookParameter> AllParameters { get; set; }

        private List<Runbook> Runbooks
        {
            get
            {
                if (this.runbooks == null)
                {
                    var hs = new HashSet<string>();
                    this.runbooks = this.RemoveDuplicate(this.CurrentFolder.Runbooks, (r) => r.Name).ToList();
                }

                return this.runbooks;
            }
        }

        private IEnumerable<Folder> Subfolders
        {
            get
            {
                if (this.subfolders == null)
                {
                    var hs = new HashSet<string>();
                    this.subfolders = this.RemoveDuplicate(this.AllFolders.Where(f => f.ParentId == this.CurrentFolder.Id), (r) => r.Name);
                }

                return this.subfolders;
            }
        }

        private IEnumerable<T> RemoveDuplicate<T>(IEnumerable<T> input, Func<T, string> checkDupOn)
        {
            var hs = new HashSet<string>();
            return input.Where(i => hs.Add(this.identifierCleaner.GetPascalName(checkDupOn(i))));
        }

        private string GetClassName()
        {
            return this.identifierCleaner.GetPascalName(this.CurrentFolder.Name);
        }

        private string GetClassMemberName()
        {
            return this.identifierCleaner.GetCamelName(this.CurrentFolder.Name);
        }

        private bool NeedToCreateInterface()
        {
            return this.Runbooks.Count > 0;
        }

        private void WriteSubfolderClass()
        {
            foreach (Folder subfolder in this.Subfolders)
            {
                FolderTemplate t = new FolderTemplate();
                t.OrchestratorProvider = this.OrchestratorProvider;
                t.AllFolders = this.AllFolders;
                t.AllParameters = this.AllParameters;
                t.CurrentFolder = subfolder;
                this.Write(t.TransformText() + "\r\n");
            }
        }

        private void WriteRunbooksFunction(bool isInterface)
        {
            foreach (Runbook runbook in this.Runbooks)
            {
                RunbookTemplate t = new RunbookTemplate();
                t.OrchestratorProvider = this.OrchestratorProvider;
                t.Runbook = runbook;
                var test = this.AllParameters.Where(rp => rp.RunbookId == runbook.Id).ToList();
                t.Parameters = this.RemoveDuplicate(this.AllParameters.Where(rp => rp.RunbookId == runbook.Id), (p) => p.Name).ToList();
                t.IsInterface = isInterface;
                this.Write(t.TransformText());
            }
        }
    }
}
