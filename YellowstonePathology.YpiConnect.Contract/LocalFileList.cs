using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract
{
	[CollectionDataContract]
	public class LocalFileList : List<LocalFile>
	{
        private bool m_IncludeMemoryStream;

        public LocalFileList()
        {

        }

		public LocalFileList(bool includeMemoryStream)
        {
            this.m_IncludeMemoryStream = includeMemoryStream;
        }

        public MethodResult Load(YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
			this.Clear();
            MethodResult methodResult = new MethodResult();

            //go get the files for this reportno
			string path = webServiceAccount.LocalFileUploadDirectory;
			if (Directory.Exists(path))
			{
				string[] files = Directory.GetFiles(path);
				foreach (string fileName in files)
				{
					LocalFile localFile = new LocalFile(fileName);
					this.Add(localFile);
					if (this.m_IncludeMemoryStream)
					{
						localFile.Load();
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
	}
}
