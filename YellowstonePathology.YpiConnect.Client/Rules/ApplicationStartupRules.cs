using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace YellowstonePathology.YpiConnect.Client.Rules
{
    public class ApplicationStartupRules
    {
        YellowstonePathology.Business.Rules.SimpleRule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
        ApplicationSettings m_ApplicationSettings;
        YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount m_WebServiceAccount;
        ContentControl m_MainWindowContent;
        YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity m_ApplicationIdentity;

        public ApplicationStartupRules(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ExecutionStatus = executionStatus;
            this.m_Rule = new Business.Rules.SimpleRule(this.m_ExecutionStatus);            

            this.m_ApplicationSettings = ApplicationSettings.Instance;
            this.m_ApplicationIdentity = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance;

            this.m_Rule.ActionList.Add(CreateApplicationRoot);
            this.m_Rule.ActionList.Add(CreateSettingsFile);
            this.m_Rule.ActionList.Add(UpgradeSettings);                        
            this.m_Rule.ActionList.Add(CreateLocalFolders);                          
        }

        public void CreateApplicationRoot() //If Necessary
        {            
            ApplicationSettings.Instance.CreateApplicationRoot();
        }

        public void CreateSettingsFile() //If Necessary
        {         
            this.m_ApplicationSettings.CreateSettingsFile();
        }

        public void UpgradeSettings()
        {         
            ApplicationSettingsUpgrade applicationSettingsUpgrade = new ApplicationSettingsUpgrade();
            applicationSettingsUpgrade.Upgrade();
        }        

        public void InstantiateSettings()
        {            
            this.m_ApplicationSettings.FromXml();
        }

        public void CreateLocalFolders() //If Necessary
        {            
            this.m_ApplicationSettings.CreateLocalFolders();
        }        

        public void Execute(ContentControl mainWindowContent)
        {
            this.m_MainWindowContent = mainWindowContent;
            this.m_Rule.Execute();
        }
    }
}
