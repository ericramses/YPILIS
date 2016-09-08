using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class PersistentDocumentIdProperty : PersistentProperty
    {
        private int m_MaxLength;

        public PersistentDocumentIdProperty(int maxLength)
        {
            this.m_MaxLength = maxLength;
        }

        public PersistentDocumentIdProperty(int maxLength, string defaultValue) :base(defaultValue)
        {
            this.m_MaxLength = maxLength;
        }

        public int MaxLength
        {
            get { return this.m_MaxLength; }
        }        
    }
}

