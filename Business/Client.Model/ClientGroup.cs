using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class ClientGroup
    {       
        private List<int> m_Members;

        public ClientGroup()
        {            
            this.m_Members = new List<int>();
        }        

        public List<int> Members
        {
            get { return this.m_Members; }
        }

        public virtual bool Exists(int clientId)
        {
            bool result = false;
            foreach (int id in this.m_Members)
            {
                if (id == clientId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
