namespace C60.OrchestratorReference
{
    partial class OrchestratorReferenceItemForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.networkCredentialGroupBox = new System.Windows.Forms.GroupBox();
            this.domainTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.serverNameTextBox = new System.Windows.Forms.TextBox();
            this.useSSLCheckBox = new System.Windows.Forms.CheckBox();
            this.runbookTreeView = new System.Windows.Forms.TreeView();
            this.label8 = new System.Windows.Forms.Label();
            this.defaultCredentialCheckBox = new System.Windows.Forms.CheckBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.portNnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.networkCredentialGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Orchestrator Server Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Use SSL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Credential";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Username";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Password";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Domain";
            // 
            // networkCredentialGroupBox
            // 
            this.networkCredentialGroupBox.Controls.Add(this.domainTextBox);
            this.networkCredentialGroupBox.Controls.Add(this.passwordTextBox);
            this.networkCredentialGroupBox.Controls.Add(this.label5);
            this.networkCredentialGroupBox.Controls.Add(this.usernameTextBox);
            this.networkCredentialGroupBox.Controls.Add(this.label7);
            this.networkCredentialGroupBox.Controls.Add(this.label6);
            this.networkCredentialGroupBox.Location = new System.Drawing.Point(158, 130);
            this.networkCredentialGroupBox.Name = "networkCredentialGroupBox";
            this.networkCredentialGroupBox.Size = new System.Drawing.Size(200, 118);
            this.networkCredentialGroupBox.TabIndex = 7;
            this.networkCredentialGroupBox.TabStop = false;
            this.networkCredentialGroupBox.Text = "Network Credential";
            this.networkCredentialGroupBox.Visible = false;
            // 
            // domainTextBox
            // 
            this.domainTextBox.Location = new System.Drawing.Point(80, 80);
            this.domainTextBox.Name = "domainTextBox";
            this.domainTextBox.Size = new System.Drawing.Size(100, 20);
            this.domainTextBox.TabIndex = 11;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(80, 50);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextBox.TabIndex = 10;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(80, 20);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(100, 20);
            this.usernameTextBox.TabIndex = 9;
            // 
            // serverNameTextBox
            // 
            this.serverNameTextBox.Location = new System.Drawing.Point(161, 15);
            this.serverNameTextBox.Name = "serverNameTextBox";
            this.serverNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.serverNameTextBox.TabIndex = 8;
            this.serverNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ServerNameTextBox_Validating);
            // 
            // useSSLCheckBox
            // 
            this.useSSLCheckBox.AutoSize = true;
            this.useSSLCheckBox.Location = new System.Drawing.Point(161, 45);
            this.useSSLCheckBox.Name = "useSSLCheckBox";
            this.useSSLCheckBox.Size = new System.Drawing.Size(15, 14);
            this.useSSLCheckBox.TabIndex = 11;
            this.useSSLCheckBox.UseVisualStyleBackColor = true;
            // 
            // runbookTreeView
            // 
            this.runbookTreeView.CheckBoxes = true;
            this.runbookTreeView.Location = new System.Drawing.Point(12, 295);
            this.runbookTreeView.Name = "runbookTreeView";
            this.runbookTreeView.Size = new System.Drawing.Size(374, 169);
            this.runbookTreeView.TabIndex = 12;
            this.runbookTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.RunbookTreeView_AfterCheck);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 276);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Include Folders";
            // 
            // defaultCredentialCheckBox
            // 
            this.defaultCredentialCheckBox.AutoSize = true;
            this.defaultCredentialCheckBox.Checked = true;
            this.defaultCredentialCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultCredentialCheckBox.Location = new System.Drawing.Point(161, 105);
            this.defaultCredentialCheckBox.Name = "defaultCredentialCheckBox";
            this.defaultCredentialCheckBox.Size = new System.Drawing.Size(110, 17);
            this.defaultCredentialCheckBox.TabIndex = 14;
            this.defaultCredentialCheckBox.Text = "Default Credential";
            this.defaultCredentialCheckBox.UseVisualStyleBackColor = true;
            this.defaultCredentialCheckBox.CheckedChanged += new System.EventHandler(this.DefaultCredentialCheckBox_CheckedChanged);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(311, 266);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 15;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // addButton
            // 
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(311, 470);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 16;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // portNnumericUpDown
            // 
            this.portNnumericUpDown.Location = new System.Drawing.Point(161, 75);
            this.portNnumericUpDown.Name = "portNnumericUpDown";
            this.portNnumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.portNnumericUpDown.TabIndex = 17;
            this.portNnumericUpDown.Value = new decimal(new int[] {
            81,
            0,
            0,
            0});
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // OrchestratorReferenceItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 501);
            this.Controls.Add(this.portNnumericUpDown);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.defaultCredentialCheckBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.runbookTreeView);
            this.Controls.Add(this.useSSLCheckBox);
            this.Controls.Add(this.serverNameTextBox);
            this.Controls.Add(this.networkCredentialGroupBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OrchestratorReferenceItemForm";
            this.Text = "Orchestrator Reference";
            this.networkCredentialGroupBox.ResumeLayout(false);
            this.networkCredentialGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox networkCredentialGroupBox;
        private System.Windows.Forms.TextBox domainTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox serverNameTextBox;
        private System.Windows.Forms.CheckBox useSSLCheckBox;
        private System.Windows.Forms.TreeView runbookTreeView;
        private System.Windows.Forms.CheckBox defaultCredentialCheckBox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.NumericUpDown portNnumericUpDown;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}