using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{    
    public class PersistentStringProperty : PersistentProperty
    {
        private int m_MaxLength;

        public PersistentStringProperty(int maxLength)
        {
            this.m_MaxLength = maxLength;            
        }        

        public int MaxLength
        {
            get { return this.m_MaxLength; }
        }        
    }
}

