using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Contract.Identity
{
    public class ApplicationIdentity
    {
        private static ApplicationIdentity m_Instance;
        private YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount m_WebServiceAccount;

        private ApplicationIdentity()
        {            

        }        

		public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount WebServiceAccount
		{
			get { return this.m_WebServiceAccount; }
            set { this.m_WebServiceAccount = value; }
		}

        public static ApplicationIdentity Instance
        {
            get { return m_Instance; }
        }
        
        public static void ReadSavedSettings()
        {            
            YellowstonePathology.YpiConnect.Contract.Identity.SavedSettings savedSettings = SavedSettings.Read();
            m_Instance = new ApplicationIdentity();
            m_Instance.WebServiceAccount = new WebServiceAccount(savedSettings);			
        }

        public static void SignIn(YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
			if (webServiceAccount.UserName == m_Instance.WebServiceAccount.UserName)
			{
				webServiceAccount.LocalFileDownloadDirectory = m_Instance.WebServiceAccount.LocalFileDownloadDirectory;
				webServiceAccount.LocalFileUploadDirectory = m_Instance.WebServiceAccount.LocalFileUploadDirectory;
			}

            m_Instance = new ApplicationIdentity();
            m_Instance.WebServiceAccount = webServiceAccount;

			if (m_Instance.WebServiceAccount.EnableSaveSettings == false)
			{
				SavedSettings.Delete();
			}
			else
			{
				m_Instance.Save();
			}
        }

        public static void SignOut()
        {            
            m_Instance = new ApplicationIdentity();
            WebServiceAccount blankWebServiceAccount = new WebServiceAccount();
            blankWebServiceAccount.IsKnown = false;
            m_Instance.WebServiceAccount = blankWebServiceAccount;                    
        }                

		public void Save(bool releaseLock)
		{						
			if (this.m_WebServiceAccount.EnableSaveSettings)
			{
                YellowstonePathology.YpiConnect.Contract.Identity.SavedSettings savedSettings = new SavedSettings(this.m_WebServiceAccount);                
                SavedSettings.Write(savedSettings);
			}			
		}				
    }
}
