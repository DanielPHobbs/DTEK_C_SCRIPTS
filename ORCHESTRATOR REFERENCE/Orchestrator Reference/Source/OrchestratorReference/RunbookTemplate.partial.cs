namespace C60.OrchestratorReference
{
    using System.Collections.Generic;
    using System.Linq;
    using C60.OrchestratorReference.OrchestratorServiceReference;

    public partial class RunbookTemplate
    {
        private IdentifierCleaner identifierCleaner = new IdentifierCleaner();

        public OrchestratorProvider OrchestratorProvider { get; set; }
        
        public Runbook Runbook { get; set; }
        
        public List<RunbookParameter> Parameters { get; set; }
        
        public bool IsInterface { get; set; }

        private string GetRunbookFuncName()
        {
            return this.identifierCleaner.GetPascalName(this.Runbook.Name);
        }

        private string GetRunbookFuncParameters()
        {
            return string.Join(", ", this.Parameters.Select(p => this.GetParameterType(p.Type) + " " + this.identifierCleaner.GetCamelName(p.Name)));
        }

        private string GetRunbookComment()
        {
            return string.Format("Execute the \"{0}\" runbook on path \"{1}\". {2}", this.Runbook.Name, this.Runbook.Path.Substring(0, this.Runbook.Path.Length - this.Runbook.Name.Length), this.Runbook.Description);
        }

        private string GetParameterType(string type)
        {
            string ret;
            switch (type)
            {
                case "String":
                    ret = "string";
                    break;
                case "Integer":
                    ret = "int";
                    break;
                case "Bool":
                    ret = "bool";
                    break;
                default:
                    ret = type;
                    break;
            }

            return ret;
        }
    }
}
