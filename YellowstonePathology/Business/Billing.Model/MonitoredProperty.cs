using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace YellowstonePathology.Business.Billing.Model
{
    public class MonitoredProperty
    {
        private PropertyInfo m_PropertyInfo;
        private object m_OrginalValue;
                
        public MonitoredProperty(PropertyInfo propertyInfo, object o)
        {
            //this.m_PropertyInfo = PropertyInfo;
            //this.m_OrginalValue = this.m_PropertyInfo.GetValue(o);
        }

        public PropertyInfo PropertyInfo
        {
            get { return this.m_PropertyInfo; }
        }

        public object OriginalValue
        {
            get { return this.m_OrginalValue; }
        }

        public bool HasChanged(object o)
        {
            bool result = false;
            if (this.m_PropertyInfo.GetValue(o) != this.OriginalValue)
            {
                result = true;
            }
            return result;
        }
    }
}
