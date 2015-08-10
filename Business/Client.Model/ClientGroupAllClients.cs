using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class ClientGroupAllClients : ClientGroup
    {
        public ClientGroupAllClients()
        {

        }

        public override bool Exists(int clientId)
        {
            return true;
        }
    }
}
