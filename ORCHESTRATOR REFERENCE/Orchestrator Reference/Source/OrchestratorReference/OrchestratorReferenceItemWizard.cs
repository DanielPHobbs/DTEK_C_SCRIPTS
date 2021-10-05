namespace C60.OrchestratorReference
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TemplateWizard;

    internal class OrchestratorReferenceItemWizard : IWizard
    {
        public void BeforeOpeningFile(EnvDTE.ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(EnvDTE.Project project)
        {
        }

        public void ProjectItemFinishedGenerating(EnvDTE.ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            try
            {
                using (OrchestratorReferenceItemForm wizardForm = new OrchestratorReferenceItemForm())
                {
                    wizardForm.ShowDialog();
                    replacementsDictionary.Add("$serverName$", wizardForm.ServerName);
                    replacementsDictionary.Add("$useSSL$", wizardForm.UseSSL.ToString().ToLower());
                    replacementsDictionary.Add("$port$", wizardForm.Port.ToString());
                    replacementsDictionary.Add("$includeFolders$", wizardForm.IncludeFolders); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
