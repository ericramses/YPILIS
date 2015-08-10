using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract
{
	[CollectionDataContract]
	public class RemoteFileList : List<RemoteFile>
    {
        private string m_ReportNo;
        private bool m_IncludeMemoryStream;

        public RemoteFileList()
        {

        }

        public RemoteFileList(string reportNo, bool includeMemoryStream)
        {
            this.m_ReportNo = reportNo;
            this.m_IncludeMemoryStream = includeMemoryStream;
        }

        public MethodResult Load()
        {
            MethodResult methodResult = new MethodResult();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_ReportNo);
			string path = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);

			if (Directory.Exists(path))
			{
				string[] files = Directory.GetFiles(path);
				foreach (string fileName in files)
				{
					RemoteFile remoteFile = new RemoteFile(fileName);
					this.Add(remoteFile);
					if (this.m_IncludeMemoryStream)
					{
						remoteFile.Load();
					}
				}
				methodResult.Success = true;
			}
			else
			{
				methodResult.Success = false;
			}
            return methodResult;
        }

		public MethodResult Load(string remoteFileUpLoadDirectory)
		{
			MethodResult methodResult = new MethodResult();

			if (Directory.Exists(remoteFileUpLoadDirectory))
			{
				string[] files = Directory.GetFiles(remoteFileUpLoadDirectory);
				foreach (string fileName in files)
				{
					RemoteFile remoteFile = new RemoteFile(fileName);
					this.Add(remoteFile);
				}
				methodResult.Success = true;
			}
			else
			{
				methodResult.Success = false;
			}
			return methodResult;
		}
    }
}
