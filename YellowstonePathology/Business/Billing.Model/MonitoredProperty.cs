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
        private Object m_Object;
        private PropertyInfo m_PropertyInfo;
        private object m_OrginalValue;
                
        public MonitoredProperty(PropertyInfo propertyInfo, object o)
        {
            this.m_Object = o;
            this.m_PropertyInfo = propertyInfo;
            this.m_OrginalValue = this.m_PropertyInfo.GetValue(o);
        }

        public PropertyInfo PropertyInfo
        {
            get { return this.m_PropertyInfo; }
        }

        public object OriginalValue
        {
            get { return this.m_OrginalValue; }
        }

        public bool HasChanged()
        {
            bool result = false;
            if (this.m_PropertyInfo.GetValue(this.m_Object) != this.OriginalValue)
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            string result = "Property: " + this.m_PropertyInfo.Name;
            return result;
        }
    }
}
