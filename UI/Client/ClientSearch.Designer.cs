namespace YellowstonePathology.UI.Client
{
    partial class ClientSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientSearch));
            this.listViewClient = new System.Windows.Forms.ListView();
            this.colClientID = new System.Windows.Forms.ColumnHeader();
            this.colClientName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTelephone = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderFax = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxClientName = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonEnvelope = new System.Windows.Forms.Button();
            this.buttonNewClient = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRequisitions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewClient
            // 
            this.listViewClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colClientID,
            this.colClientName,
            this.columnHeaderTelephone,
            this.columnHeaderFax});
            this.listViewClient.FullRowSelect = true;
            this.listViewClient.HideSelection = false;
            this.listViewClient.Location = new System.Drawing.Point(19, 40);
            this.listViewClient.Name = "listViewClient";
            this.listViewClient.Size = new System.Drawing.Size(572, 203);
            this.listViewClient.TabIndex = 9;
            this.listViewClient.TabStop = false;
            this.listViewClient.UseCompatibleStateImageBehavior = false;
            this.listViewClient.View = System.Windows.Forms.View.Details;
            this.listViewClient.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewClient_MouseDoubleClick);
            this.listViewClient.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewClient_MouseClick);
            // 
            // colClientID
            // 
            this.colClientID.Text = "ID";
            this.colClientID.Width = 48;
            // 
            // colClientName
            // 
            this.colClientName.Text = "Client Name";
            this.colClientName.Width = 243;
            // 
            // columnHeaderTelephone
            // 
            this.columnHeaderTelephone.Text = "Phone";
            this.columnHeaderTelephone.Width = 114;
            // 
            // columnHeaderFax
            // 
            this.columnHeaderFax.Text = "Fax";
            this.columnHeaderFax.Width = 114;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Client Name:";
            // 
            // textBoxClientName
            // 
            this.textBoxClientName.Location = new System.Drawing.Point(89, 14);
            this.textBoxClientName.Name = "textBoxClientName";
            this.textBoxClientName.Size = new System.Drawing.Size(297, 20);
            this.textBoxClientName.TabIndex = 7;
            this.textBoxClientName.TextChanged += new System.EventHandler(this.textBoxClientName_TextChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(516, 249);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonEnvelope
            // 
            this.buttonEnvelope.Location = new System.Drawing.Point(19, 249);
            this.buttonEnvelope.Name = "buttonEnvelope";
            this.buttonEnvelope.Size = new System.Drawing.Size(75, 23);
            this.buttonEnvelope.TabIndex = 11;
            this.buttonEnvelope.Text = "Envelope";
            this.buttonEnvelope.UseVisualStyleBackColor = true;
            this.buttonEnvelope.Click += new System.EventHandler(this.buttonEnvelope_Click);
            // 
            // buttonNewClient
            // 
            this.buttonNewClient.Location = new System.Drawing.Point(100, 249);
            this.buttonNewClient.Name = "buttonNewClient";
            this.buttonNewClient.Size = new System.Drawing.Size(75, 23);
            this.buttonNewClient.TabIndex = 12;
            this.buttonNewClient.Text = "New";
            this.buttonNewClient.UseVisualStyleBackColor = true;
            this.buttonNewClient.Click += new System.EventHandler(this.buttonNewClient_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(181, 249);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 13;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonRequisitions
            // 
            this.buttonRequisitions.Location = new System.Drawing.Point(333, 249);
            this.buttonRequisitions.Name = "buttonRequisitions";
            this.buttonRequisitions.Size = new System.Drawing.Size(75, 23);
            this.buttonRequisitions.TabIndex = 14;
            this.buttonRequisitions.Text = "Requisitions";
            this.buttonRequisitions.UseVisualStyleBackColor = true;
            this.buttonRequisitions.Click += new System.EventHandler(this.buttonRequisitions_Click);
            // 
            // ClientSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 288);
            this.Controls.Add(this.buttonRequisitions);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonNewClient);
            this.Controls.Add(this.buttonEnvelope);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.listViewClient);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxClientName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClientSearch";
            this.ShowInTaskbar = false;
            this.Text = "Client Search";
            this.Activated += new System.EventHandler(this.ClientSearch_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewClient;
        private System.Windows.Forms.ColumnHeader colClientID;
        private System.Windows.Forms.ColumnHeader colClientName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxClientName;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ColumnHeader columnHeaderTelephone;
        private System.Windows.Forms.ColumnHeader columnHeaderFax;
        private System.Windows.Forms.Button buttonEnvelope;
        private System.Windows.Forms.Button buttonNewClient;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRequisitions;

    }
}