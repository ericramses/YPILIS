using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract
{
    [DataContract]
    public class RemoteFile
    {
        const string FileNotFoundMessage = "There was a problem finding your document. An email has been sent to support. We will look into why your report is not available. You can call (406)238-6360 or try again in a few minutes.";

        private System.IO.MemoryStream m_MemoryStream;
        private string m_FullPath;
        private bool m_IsDownloaded;
        private string m_TempFileName;
        private YellowstonePathology.YpiConnect.Contract.CaseDocumentTypeEnum m_CaseDocumentType;
        private string m_ReportNo;
        private bool m_UseReportNoToFindFile;

        public RemoteFile(string fullPath)
        {
            this.m_FullPath = fullPath;
            this.m_UseReportNoToFindFile = false;
        }

        public RemoteFile(string reportNo, YellowstonePathology.YpiConnect.Contract.CaseDocumentTypeEnum caseDocumentType)
        {
            this.m_ReportNo = reportNo;
            this.m_CaseDocumentType = caseDocumentType;
            this.m_UseReportNoToFindFile = true;
        }

		public RemoteFile(LocalFile localFile, Identity.WebServiceAccount webServiceAccount)
		{
			this.m_FullPath = System.IO.Path.Combine(webServiceAccount.RemoteFileUploadDirectory, localFile.FileName);
			this.m_MemoryStream = localFile.MemoryStream;
		}

		public MethodResult Load()
        {
            MethodResult methodResult = new MethodResult();
            this.m_MemoryStream = new System.IO.MemoryStream();
            if (System.IO.File.Exists(this.m_FullPath) == true)
            {
				System.IO.FileStream fileStream = System.IO.File.OpenRead(this.m_FullPath);
                FileHelper.CopyStream(fileStream, MemoryStream);
                fileStream.Close();
                methodResult.Success = true;
            }
            else
            {                
                methodResult.Message = FileNotFoundMessage;
                methodResult.Success = false;
            }
            return methodResult;
        }

		public MethodResult Save()
		{
			MethodResult methodResult = new MethodResult();
			System.IO.FileStream fileStream = new System.IO.FileStream(this.FullPath, System.IO.FileMode.Create);
			byte[] bytes = this.m_MemoryStream.ToArray();
			fileStream.Write(bytes, 0, bytes.Length);
			fileStream.Close();
			methodResult.Success = true;
			return methodResult;
		}

        public void SaveTemp()
        {
            this.m_TempFileName = System.IO.Path.GetTempFileName();
            System.IO.FileStream fileStream = new System.IO.FileStream(this.m_TempFileName, System.IO.FileMode.Create);
            byte[] bytes = this.m_MemoryStream.ToArray();
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();            
        }

        public void SaveTempPdf()
        {
            this.m_TempFileName = System.IO.Path.GetTempFileName() + ".pdf";
            System.IO.FileStream fileStream = new System.IO.FileStream(this.m_TempFileName, System.IO.FileMode.Create);
            byte[] bytes = this.m_MemoryStream.ToArray();
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
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
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }
               
        [DataMember]
        public bool IsDownloaded
        {
            get { return this.m_IsDownloaded; }
            set { this.m_IsDownloaded = value; }
        }

        [DataMember]
        public CaseDocumentTypeEnum CaseDocumentType
        {
            get { return this.m_CaseDocumentType; }
            set { this.m_CaseDocumentType = value; }
        }

        public string TempFileName
        {
            get { return this.m_TempFileName; }
            set { this.m_TempFileName = value; }
        }

        [DataMember]
        public bool UseReportNoToFindFile
        {
            get { return this.m_UseReportNoToFindFile; }
            set { this.m_UseReportNoToFindFile = value; }
        }

        public string FileName
        {
            get
            {
                return System.IO.Path.GetFileName(this.m_FullPath);
            }
        }

        public string Extension
        {
            get
            {
				return System.IO.Path.GetExtension(this.m_FullPath).ToUpper();
            }
        }
    }
}
