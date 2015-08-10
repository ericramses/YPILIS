namespace YellowstonePathology.UI.Client
{
    partial class ReportDistribution
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
            this.listViewDistribution = new System.Windows.Forms.ListView();
            this.colPhysicianClientID = new System.Windows.Forms.ColumnHeader();
            this.colPhysicianName = new System.Windows.Forms.ColumnHeader();
            this.colClientName = new System.Windows.Forms.ColumnHeader();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonAddOther = new System.Windows.Forms.Button();
            this.listViewPhysicianClient = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewDistribution
            // 
            this.listViewDistribution.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPhysicianClientID,
            this.colPhysicianName,
            this.colClientName});
            this.listViewDistribution.FullRowSelect = true;
            this.listViewDistribution.HideSelection = false;
            this.listViewDistribution.Location = new System.Drawing.Point(12, 12);
            this.listViewDistribution.Name = "listViewDistribution";
            this.listViewDistribution.Size = new System.Drawing.Size(638, 105);
            this.listViewDistribution.TabIndex = 15;
            this.listViewDistribution.TabStop = false;
            this.listViewDistribution.UseCompatibleStateImageBehavior = false;
            this.listViewDistribution.View = System.Windows.Forms.View.Details;
            // 
            // colPhysicianClientID
            // 
            this.colPhysicianClientID.Text = "ID";
            // 
            // colPhysicianName
            // 
            this.colPhysicianName.Text = "Physician Name";
            this.colPhysicianName.Width = 231;
            // 
            // colClientName
            // 
            this.colClientName.Text = "Client Name";
            this.colClientName.Width = 299;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(575, 234);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 16;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonAddOther
            // 
            this.buttonAddOther.Location = new System.Drawing.Point(93, 234);
            this.buttonAddOther.Name = "buttonAddOther";
            this.buttonAddOther.Size = new System.Drawing.Size(75, 23);
            this.buttonAddOther.TabIndex = 17;
            this.buttonAddOther.Text = "Add Other";
            this.buttonAddOther.UseVisualStyleBackColor = true;
            this.buttonAddOther.Click += new System.EventHandler(this.buttonAddOther_Click);
            // 
            // listViewPhysicianClient
            // 
            this.listViewPhysicianClient.BackColor = System.Drawing.SystemColors.Info;
            this.listViewPhysicianClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewPhysicianClient.FullRowSelect = true;
            this.listViewPhysicianClient.HideSelection = false;
            this.listViewPhysicianClient.Location = new System.Drawing.Point(12, 123);
            this.listViewPhysicianClient.Name = "listViewPhysicianClient";
            this.listViewPhysicianClient.Size = new System.Drawing.Size(638, 105);
            this.listViewPhysicianClient.TabIndex = 18;
            this.listViewPhysicianClient.TabStop = false;
            this.listViewPhysicianClient.UseCompatibleStateImageBehavior = false;
            this.listViewPhysicianClient.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Physician Name";
            this.columnHeader2.Width = 231;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Client Name";
            this.columnHeader3.Width = 299;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(174, 234);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 19;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 234);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 20;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // ReportDistribution
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 267);
            this.ControlBox = false;
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.listViewPhysicianClient);
            this.Controls.Add(this.buttonAddOther);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.listViewDistribution);
            this.Name = "ReportDistribution";
            this.ShowInTaskbar = false;
            this.Text = "Report Distribution";
            this.Load += new System.EventHandler(this.ReportDistribution_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewDistribution;
        private System.Windows.Forms.ColumnHeader colPhysicianName;
        private System.Windows.Forms.ColumnHeader colClientName;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ColumnHeader colPhysicianClientID;
        private System.Windows.Forms.Button buttonAddOther;
        private System.Windows.Forms.ListView listViewPhysicianClient;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
    }
}