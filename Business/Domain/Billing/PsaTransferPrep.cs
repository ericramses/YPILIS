using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace YellowstonePathology.Business.Domain.Billing
{
	public class PsaTransferPrep
	{
		public event PsaTransferEventHandler PsaTransferEvent;
		public delegate void PsaTransferEventHandler(string message);

        private ReportNoCollection m_ReportNoCollection;        
		
		private string m_UserName = "YELLOWSTONE";
		private string m_Password = "Y0g1B34r";
		private string m_FtpAddress = @"ftp.psapath.com";

		private DateTime m_PostDate;

        private string m_BaseWorkingFolderPath = @"\\CFileServer\Documents\Billing\PSA";
        private string m_WorkingFolder;

        public PsaTransferPrep(ReportNoCollection reportNoCollection, DateTime postDate)
		{            
            this.m_ReportNoCollection = reportNoCollection;
            this.m_PostDate = postDate;
            this.m_WorkingFolder = Path.Combine(this.m_BaseWorkingFolderPath, this.m_PostDate.ToString("MMddyyyy"));
		}

        public void PrepareForTransfer()
        {                      
            
        }

        private void SendEvent(string message)
        {
            if (PsaTransferEvent != null)
            {
                PsaTransferEvent(message);
            }
        }
       
	}
}
