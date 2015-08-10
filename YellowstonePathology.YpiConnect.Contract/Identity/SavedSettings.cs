using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;

namespace YellowstonePathology.YpiConnect.Contract.Identity
{
    [Serializable]
    public class SavedSettings
    {
        public const string FileName = "SavedSettings.dat";
        
        string m_UserName;
        string m_Password;		
        string m_LocalFileDownloadDirectory;
		string m_LocalFileUploadDirectory;

        public SavedSettings()
        {

        }

        public SavedSettings(YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            if (webServiceAccount.SaveUserNameLocal)
            {
                this.m_UserName = webServiceAccount.UserName;
            }
            else
            {
                this.m_UserName = string.Empty;
            }
            if (webServiceAccount.SavePasswordLocal)
            {
                this.m_Password = webServiceAccount.Password;
            }
            else
            {
                this.m_Password = string.Empty;
            }
            if (webServiceAccount.EnableFileDownload == true)
            {
				this.m_LocalFileDownloadDirectory = webServiceAccount.LocalFileDownloadDirectory;
            }
            else
            {
				this.m_LocalFileDownloadDirectory = string.Empty;
            }
			if (webServiceAccount.EnableFileUpload == true)
			{
				this.m_LocalFileUploadDirectory = webServiceAccount.LocalFileUploadDirectory;
			}
			else
			{
				this.m_LocalFileUploadDirectory = string.Empty;
			}
		}
        
        public string UserName
        {
            get { return this.m_UserName; }
            set { this.m_UserName = value;  }
        }

        public string Password
        {
            get { return this.m_Password; }
            set { this.m_Password = value; }
        }		

        public string LocalFileDownloadDirectory
        {
			get { return this.m_LocalFileDownloadDirectory; }
			set { this.m_LocalFileDownloadDirectory = value; }
        }

		public string LocalFileUploadDirectory
		{
			get { return this.m_LocalFileUploadDirectory; }
			set { this.m_LocalFileUploadDirectory = value; }
		}

        public static void Delete()
        {
            try
            {
                IsolatedStorageFile isf = System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
                isf.DeleteFile(FileName);                
            }
            catch 
            {
                
            }
        }

        public static SavedSettings Read()
        {
            SavedSettings savedSettings = null;
            try
            {
                IsolatedStorageFile isf = System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
                System.IO.Stream reader = new IsolatedStorageFileStream(SavedSettings.FileName, System.IO.FileMode.Open, isf);
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                savedSettings = (SavedSettings)formatter.Deserialize(reader);
                reader.Close();

				CreateLocalFolders(savedSettings);
            }
            catch (System.IO.FileNotFoundException)
            {                
                savedSettings = new SavedSettings();                                
            }
            return savedSettings;
        }

        public static void Write(SavedSettings savedSettings)
        {
            IsolatedStorageFile isf = System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            System.IO.Stream writer = new IsolatedStorageFileStream(SavedSettings.FileName, System.IO.FileMode.Create, isf);
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(writer, savedSettings);
            writer.Close();

			CreateLocalFolders(savedSettings);
		}

		private static void CreateLocalFolders(SavedSettings savedSettings)
		{
			if (!string.IsNullOrEmpty(savedSettings.LocalFileDownloadDirectory))
			{
				if (!System.IO.Directory.Exists(savedSettings.LocalFileDownloadDirectory))
				{
					System.IO.Directory.CreateDirectory(savedSettings.LocalFileDownloadDirectory);
				}
			}
		}
    }
}
