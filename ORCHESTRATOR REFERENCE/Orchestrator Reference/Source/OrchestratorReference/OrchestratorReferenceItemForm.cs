namespace C60.OrchestratorReference
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using C60.OrchestratorReference.OrchestratorServiceReference;

    public partial class OrchestratorReferenceItemForm : Form
    {
        public OrchestratorReferenceItemForm()
        {
            InitializeComponent();
        }

        public string ServerName { get; set; }
        
        public bool UseSSL { get; set; }
        
        public int Port { get; set; }
        
        public string IncludeFolders { get; set; }

        private string GetSelectedFolders(TreeNodeCollection treeNodeCollection)
        {
            string result = string.Empty;
            foreach (TreeNode node in treeNodeCollection)
            {
                if (node.Checked && this.AllSubNodeSelected(node))
                {
                    result += node.Tag.ToString() + ",";
                }
                else
                {
                    result += this.GetSelectedFolders(node.Nodes);
                }
            }

            return result;
        }

        private bool AllSubNodeSelected(TreeNode node)
        {
            bool result = true;

            for (int i = 0; i < node.Nodes.Count; i++)
            {
                if ((node.Nodes[i].Nodes.Count == 0 && !node.Nodes[i].Checked) || !this.AllSubNodeSelected(node.Nodes[i]))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            this.runbookTreeView.BeginUpdate();
            Task.Factory.StartNew(() =>
                {
                    try
                    {
                        NetworkCredential cred = null;
                        if (!this.defaultCredentialCheckBox.Checked)
                        {
                            cred = new NetworkCredential(this.usernameTextBox.Text, this.passwordTextBox.Text, this.domainTextBox.Text);
                        }

                        OrchestratorProvider provider = new OrchestratorProvider(this.useSSLCheckBox.Checked, this.serverNameTextBox.Text, (int)this.portNnumericUpDown.Value, cred);
                        List<Folder> allFolders = provider.GetFoldersAndRunbooks();
                        Folder rootFolder = allFolders.Single(f => f.ParentId == null);
                        PopulateRunbookTree(allFolders, rootFolder, this.runbookTreeView.Nodes);
                        this.Invoke((Action)(() =>
                            {
                                this.runbookTreeView.EndUpdate();
                                this.addButton.Enabled = true;
                                this.Cursor = Cursors.Default;
                            }));
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((Action)(() =>
                            {
                                MessageBox.Show(ex.Message);
                                this.Cursor = Cursors.Default;
                            }));
                    }
                });
        }
        
        private void PopulateRunbookTree(List<Folder> allFolders, Folder rootFolder, TreeNodeCollection treeNodeCollection)
        {
            var subFolders = allFolders.Where(f => f.ParentId == rootFolder.Id).OrderBy(f => f.Name);
            foreach (Folder folder in subFolders)
            {
                TreeNode tn = new TreeNode(folder.Name);
                tn.Checked = true;
                tn.Tag = folder.Path;
                this.runbookTreeView.Invoke((Action)(() => treeNodeCollection.Add(tn)));
                this.PopulateRunbookTree(allFolders, folder, tn.Nodes);
            }
        }

        private void ServerNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.serverNameTextBox.Text))
            {
                this.errorProvider.SetError(this.serverNameTextBox, "Server name is required");
            }
            else
            {
                this.errorProvider.SetError(this.serverNameTextBox, string.Empty);
            }
        }

        private void RunbookTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool nodeChecked = e.Node.Checked;
            this.SetSubNode(e.Node, nodeChecked);
        }

        private void SetSubNode(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode subNode in treeNode.Nodes)
            {
                subNode.Checked = nodeChecked;
                this.SetSubNode(subNode, nodeChecked);
            }
        }

        private void DefaultCredentialCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.defaultCredentialCheckBox.Checked)
            {
                this.networkCredentialGroupBox.Visible = false;
            }
            else
            {
                this.networkCredentialGroupBox.Visible = true;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            this.ServerName = this.serverNameTextBox.Text;
            this.UseSSL = this.useSSLCheckBox.Checked;
            this.Port = (int)this.portNnumericUpDown.Value;
            this.IncludeFolders = this.GetSelectedFolders(this.runbookTreeView.Nodes).TrimEnd(',');
            this.Dispose();
        }
    }
}
