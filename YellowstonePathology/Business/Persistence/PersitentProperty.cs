using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{    
    public class PersistentProperty : System.Attribute
    {        
        private bool m_IsInheritingClassKey;
        private string m_DefaultValue;

        public PersistentProperty()
        {
            this.m_IsInheritingClassKey = false;
            this.m_DefaultValue = null;
        }

        public PersistentProperty(bool isInheritingClassKey)
        {
            this.m_IsInheritingClassKey = isInheritingClassKey;
            this.m_DefaultValue = null;
        }

        public PersistentProperty(bool isInheritingClassKey, string defaultValue)
        {
            this.m_IsInheritingClassKey = isInheritingClassKey;
            this.m_DefaultValue = defaultValue;
        }

        public PersistentProperty(string defaultValue)
        {
            this.m_IsInheritingClassKey = false;
            this.m_DefaultValue = defaultValue;
        }

        public bool IsInheritingClassKey
        {
            get { return this.m_IsInheritingClassKey; }
        }

        public string DefaultValue
        {
            get { return this.m_DefaultValue; }
        }
    }
}

