namespace YellowstonePathology.UI.Client
{
    partial class PhysicianClientSearch
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.listViewPhysicianClient = new System.Windows.Forms.ListView();
            this.colPhysicianClientID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClientID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPhysicianID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClientName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFirstName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPhysicianName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonDistribution = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(628, 242);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(547, 242);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // listViewPhysicianClient
            // 
            this.listViewPhysicianClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPhysicianClientID,
            this.colClientID,
            this.colPhysicianID,
            this.colClientName,
            this.colFirstName,
            this.colLastName});
            this.listViewPhysicianClient.FullRowSelect = true;
            this.listViewPhysicianClient.HideSelection = false;
            this.listViewPhysicianClient.Location = new System.Drawing.Point(12, 35);
            this.listViewPhysicianClient.Name = "listViewPhysicianClient";
            this.listViewPhysicianClient.Size = new System.Drawing.Size(691, 203);
            this.listViewPhysicianClient.TabIndex = 1;
            this.listViewPhysicianClient.UseCompatibleStateImageBehavior = false;
            this.listViewPhysicianClient.View = System.Windows.Forms.View.Details;
            // 
            // colPhysicianClientID
            // 
            this.colPhysicianClientID.Text = "ID";
            this.colPhysicianClientID.Width = 59;
            // 
            // colClientID
            // 
            this.colClientID.Text = "ClientID";
            this.colClientID.Width = 0;
            // 
            // colPhysicianID
            // 
            this.colPhysicianID.Text = "PhysicianID";
            this.colPhysicianID.Width = 0;
            // 
            // colClientName
            // 
            this.colClientName.Text = "Client Name";
            this.colClientName.Width = 261;
            // 
            // colFirstName
            // 
            this.colFirstName.Text = "First Name";
            this.colFirstName.Width = 155;
            // 
            // colLastName
            // 
            this.colLastName.Text = "Last Name";
            this.colLastName.Width = 175;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, -15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Client:";
            // 
            // textBoxPhysicianName
            // 
            this.textBoxPhysicianName.Location = new System.Drawing.Point(117, 9);
            this.textBoxPhysicianName.Name = "textBoxPhysicianName";
            this.textBoxPhysicianName.Size = new System.Drawing.Size(586, 20);
            this.textBoxPhysicianName.TabIndex = 0;
            this.textBoxPhysicianName.TextChanged += new System.EventHandler(this.textBoxPhysicianName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Client/Physician:";
            // 
            // buttonDistribution
            // 
            this.buttonDistribution.Location = new System.Drawing.Point(18, 242);
            this.buttonDistribution.Name = "buttonDistribution";
            this.buttonDistribution.Size = new System.Drawing.Size(75, 23);
            this.buttonDistribution.TabIndex = 18;
            this.buttonDistribution.TabStop = false;
            this.buttonDistribution.Text = "Distribution";
            this.buttonDistribution.UseVisualStyleBackColor = true;
            this.buttonDistribution.Click += new System.EventHandler(this.buttonDistribution_Click);
            // 
            // PhysicianClientSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 273);
            this.ControlBox = false;
            this.Controls.Add(this.buttonDistribution);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.listViewPhysicianClient);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPhysicianName);
            this.Name = "PhysicianClientSearch";
            this.Text = "Client PhysicianSearch";
            this.Load += new System.EventHandler(this.PhysicianClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ListView listViewPhysicianClient;
        private System.Windows.Forms.ColumnHeader colPhysicianClientID;
        private System.Windows.Forms.ColumnHeader colClientName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPhysicianName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonDistribution;
        private System.Windows.Forms.ColumnHeader colClientID;
        private System.Windows.Forms.ColumnHeader colPhysicianID;
        private System.Windows.Forms.ColumnHeader colFirstName;
		private System.Windows.Forms.ColumnHeader colLastName;        
    }
}