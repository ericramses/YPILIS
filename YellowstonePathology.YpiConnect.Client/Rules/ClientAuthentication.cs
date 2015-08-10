using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace YellowstonePathology.YpiConnect.Client.Rules
{
    public class ClientAuthentication
    {
        YellowstonePathology.Business.Rules.SimpleRule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
        ApplicationSettings m_ApplicationSettings;
        YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount m_WebServiceAccount;        
        YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity m_ApplicationIdentity;

        string m_UserNameToValidate;
        string m_PasswordToValidate;        

        public ClientAuthentication(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ExecutionStatus = executionStatus;
            this.m_ApplicationSettings = ApplicationSettings.Instance;
            this.m_ApplicationIdentity = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance;
            this.m_Rule = new Business.Rules.SimpleRule(this.m_ExecutionStatus);


            this.m_Rule.ActionList.Add(ValidateWithWebService);

            //this.m_Rule.ActionList.Add(LookupDomainAccount);        
            //this.m_Rule.ActionList.Add(IsPasswordCorret);                        
            //this.m_Rule.ActionList.Add(SaveCurrentSignInfo);   
        }

        private void ValidateWithWebService()
        {
            /*
            YellowstonePathology.YpiConnect.Proxy.WebServiceAccountServiceProxy clientUserServiceProxy = new YpiConnect.Proxy.WebServiceAccountServiceProxy();
            clientUserServiceProxy.CreateChannel(this.m_ApplicationIdentity.DomainAccount);
            bool result = clientUserServiceProxy.IsClientAuthenticated(this.m_ApplicationIdentity.DomainAccount.SignInName, this.m_ApplicationIdentity.DomainAccount.Password);

            if (result == false)
            {
                YellowstonePathology.YpiConnect.Contract.Identity.GuestDomainAccount guestDomainAccount = new YpiConnect.Contract.Identity.GuestDomainAccount();
                this.m_ExecutionStatus.FailureMessage = "Authentication with this Username/Password combination failed.";
                this.m_Rule.ExecutionStatus.AddMessage("Client authentication failed: " + this.m_ApplicationIdentity.DomainAccount.UserName + " - " + this.m_ApplicationIdentity.DomainAccount.Password, true);
                YellowstonePathology.YpiConnect.Proxy.ClientServicesLogServiceProxy clientServicesLogServiceProxy = new YpiConnect.Proxy.ClientServicesLogServiceProxy();
                clientServicesLogServiceProxy.CreateChannel(guestDomainAccount);
                clientServicesLogServiceProxy.LogEvent(1061, this.m_ApplicationIdentity.DomainAccount.GetUserNamePasswordString());
            }
             */
        }

        /*
        private void LookupDomainAccount()
        {
            this.m_ApplicationIdentity.DomainAccount = this.m_WebServiceAccount.GetDomainAccount(this.m_UserNameToValidate);
            if (this.m_ApplicationIdentity.DomainAccount == null)
            {
                this.m_ExecutionStatus.FailureMessage = "The Username/Password combination was not recognized.";
                this.m_ExecutionStatus.AddMessage("Password is not valid.", true);
            }
        }        

        private void IsPasswordCorret()
        {
            if (this.m_PasswordToValidate != this.m_ApplicationIdentity.DomainAccount.Password)
            {
                this.m_ExecutionStatus.FailureMessage = "The Username/Password combination was not recognized.";
                this.m_ExecutionStatus.AddMessage("Password is not valid.", true);
            }
        }
        
        private void ValidateWithWebService()
        {
            
            YellowstonePathology.YpiConnect.Proxy.WebServiceAccountServiceProxy clientUserServiceProxy = new YpiConnect.Proxy.WebServiceAccountServiceProxy();
            clientUserServiceProxy.CreateChannel(this.m_ApplicationIdentity.DomainAccount);
            bool result = clientUserServiceProxy.IsClientAuthenticated(this.m_ApplicationIdentity.DomainAccount.SignInName, this.m_ApplicationIdentity.DomainAccount.Password);

            if (result == false)
            {
                YellowstonePathology.YpiConnect.Contract.Identity.GuestDomainAccount guestDomainAccount = new YpiConnect.Contract.Identity.GuestDomainAccount();
                this.m_ExecutionStatus.FailureMessage = "Authentication with this Username/Password combination failed.";
                this.m_Rule.ExecutionStatus.AddMessage("Client authentication failed: " + this.m_ApplicationIdentity.DomainAccount.UserName + " - " + this.m_ApplicationIdentity.DomainAccount.Password, true);
                YellowstonePathology.YpiConnect.Proxy.ClientServicesLogServiceProxy clientServicesLogServiceProxy = new YpiConnect.Proxy.ClientServicesLogServiceProxy();
                clientServicesLogServiceProxy.CreateChannel(guestDomainAccount);
                clientServicesLogServiceProxy.LogEvent(1061, this.m_ApplicationIdentity.DomainAccount.GetUserNamePasswordString());
            }
        }

        private void SaveCurrentSignInfo()
        {
            this.m_ApplicationSettings.ClientUserName = this.m_ApplicationIdentity.DomainAccount.UserName;
            this.m_ApplicationSettings.ClientPassword = this.m_ApplicationIdentity.DomainAccount.Password;
            this.m_ApplicationSettings.Save();
        }
        */
        
        public void Execute(string userNameToValidate, string passwordToValidate, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            this.m_UserNameToValidate = userNameToValidate;
            this.m_PasswordToValidate = passwordToValidate;
            this.m_WebServiceAccount = webServiceAccount;            
            this.m_Rule.Execute();
        }
    }
}
