using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class PhysicianClientReturnEventArgs : System.EventArgs
    {
        private Business.Client.PhysicianClient m_PhysicianClient;

        public PhysicianClientReturnEventArgs(Business.Client.PhysicianClient physicianClient)
        {
            this.m_PhysicianClient = physicianClient;
        }

        public Business.Client.PhysicianClient PhysicianClient
        {
            get { return this.m_PhysicianClient; }
        }
    }
}
