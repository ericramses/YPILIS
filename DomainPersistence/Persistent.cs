using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public class PersistentProperty : System.Attribute
    {
        private bool m_IsPrimaryKey;

        public PersistentProperty(bool isPrimaryKey)
        {
            this.m_IsPrimaryKey = isPrimaryKey;
        }

        public bool IsPrimaryKey
        {
            get { return this.m_IsPrimaryKey; }
        }
    }
}
