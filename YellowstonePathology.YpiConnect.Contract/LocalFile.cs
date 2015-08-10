using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract
{
	[DataContract]
	public class LocalFile
    {
        private System.IO.MemoryStream m_MemoryStream;
        private string m_FullPath;
        private bool m_IsUploaded;

        public LocalFile(string fullPath)
        {
			this.m_FullPath = fullPath;
        }
       
        public LocalFile(RemoteFile remoteFile, string fullPath)
        {
            this.m_FullPath = fullPath;
            this.m_MemoryStream = remoteFile.MemoryStream;
        }

		public LocalFile(RemoteFile remoteFile, Identity.WebServiceAccount webServiceAccount)
		{
			this.m_FullPath = System.IO.Path.Combine(webServiceAccount.LocalFileDownloadDirectory, remoteFile.FileName);
            this.m_MemoryStream = remoteFile.MemoryStream;
		}

		[DataMember]
		public System.IO.MemoryStream MemoryStream
        {
            get { return this.m_MemoryStream; }
            set { this.m_MemoryStream = value; }
        }

		[DataMember]
		public string FullPath
		{
			get { return this.m_FullPath; }
			set { this.m_FullPath = value; }
		}

		[DataMember]
		public bool IsUploaded
        {
            get { return this.m_IsUploaded; }
            set { this.m_IsUploaded = value; }
        }

		public string FileName
        {
			get { return System.IO.Path.GetFileName(this.m_FullPath); }
        }

		public void Load()
		{
			if (System.IO.File.Exists(this.FullPath) == true)
			{
				System.IO.FileStream fileStream = System.IO.File.OpenRead(this.m_FullPath);
				this.m_MemoryStream = new System.IO.MemoryStream();
				FileHelper.CopyStream(fileStream, MemoryStream);
				fileStream.Close();
			}
		}

		public void Save()
		{			
            System.IO.FileStream fileStream = new System.IO.FileStream(this.FullPath, System.IO.FileMode.Create);
            byte [] bytes = this.m_MemoryStream.ToArray();
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();		
		}        
	}
}
