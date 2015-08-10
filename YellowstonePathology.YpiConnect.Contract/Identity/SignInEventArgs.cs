using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Contract.Identity
{
    public class SignInEventArgs
    {
        private YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount m_WebServiceAccount;

        public SignInEventArgs(YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            this.m_WebServiceAccount = webServiceAccount;
        }

        public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount WebServiceAccount
        {
            get { return this.m_WebServiceAccount; }
        }
    }
}
