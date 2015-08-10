using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace YellowstonePathology.YpiConnect.Client.Rules
{
    public class SignInRules
    {
        YellowstonePathology.Business.Rules.SimpleRule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        ApplicationSettings m_ApplicationSettings;
        YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount m_WebServiceAccount;
        ContentControl m_MainWindowContent;
        YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity m_ApplicationIdentity;

        private const string AccountKeyNotSetMessage = "We are unable to accept login requests because the Account Key is not set.\r\n Please contact YPI Support at (406)238-6050";
        private const string AccountKeyNotRecognizedMessage = "We are unable to accept login requests because the Account Key was not recognized.\r\n Please contact YPI Support at (406)238-6050";

        public SignInRules(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ExecutionStatus = executionStatus;
            this.m_Rule = new Business.Rules.SimpleRule(this.m_ExecutionStatus);

            this.m_ApplicationSettings = ApplicationSettings.Instance;
            this.m_ApplicationIdentity = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance;

            this.m_Rule.ActionList.Add(IsAccountKeySet);
            this.m_Rule.ActionList.Add(LookupWebServiceAccount);
            this.m_Rule.ActionList.Add(SetDomainAccount);          
        }

        public void IsAccountKeySet()
        {
            if (string.IsNullOrEmpty(this.m_ApplicationSettings.AccountKey) == true)
            {
                this.m_ExecutionStatus.AddMessage(AccountKeyNotSetMessage, true);
                this.m_ExecutionStatus.FailureMessage = AccountKeyNotSetMessage;
            }
        }

        public void LookupWebServiceAccount()
        {
            //YellowstonePathology.YpiConnect.Contract.Identity.GuestDomainAccount guestDomainAccount = new YpiConnect.Contract.Identity.GuestDomainAccount();
            //YellowstonePathology.YpiConnect.Proxy.WebServiceAccountServiceProxy clientUserProxy = new YpiConnect.Proxy.WebServiceAccountServiceProxy();
            //clientUserProxy.CreateChannel();

            /*
            this.m_WebServiceAccount = clientUserProxy.GetWebServiceAccount(this.m_ApplicationSettings.AccountKey);
            if (this.m_WebServiceAccount == null)
            {
                this.m_ExecutionStatus.AddMessage(AccountKeyNotRecognizedMessage, true);
                this.m_ExecutionStatus.FailureMessage = AccountKeyNotRecognizedMessage;
            }
            else
            {
                this.m_ApplicationIdentity.WebServiceAccount = this.m_WebServiceAccount;
            }
            */
        }

        public void SetDomainAccount()
        {
            /*
            if (this.m_ApplicationSettings.ClientUserName != null)
            {
                YellowstonePathology.YpiConnect.Contract.Identity.DomainAccount domainAccount = this.m_WebServiceAccount.GetDomainAccount(this.m_ApplicationSettings.ClientUserName);
                if (domainAccount != null)
                {
                    this.m_ApplicationIdentity.DomainAccount = domainAccount;
                }
            }
            */
        }

        public void Execute(ContentControl mainWindowContent)
        {
            this.m_MainWindowContent = mainWindowContent;
            this.m_Rule.Execute();
        }
    }
}
