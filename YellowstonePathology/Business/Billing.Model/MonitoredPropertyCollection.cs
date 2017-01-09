using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace YellowstonePathology.Business.Billing.Model
{
    public class MonitoredPropertyCollection : ObservableCollection<MonitoredProperty>
    {
        public void Load(Type type, object o)
        {            
            PropertyInfo[] properties = type.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.MonitorProperty))).ToArray();
            foreach (PropertyInfo property in properties)
            {
                MonitoredProperty monitoredProperty = new MonitoredProperty(property, o);
                this.Add(monitoredProperty);
            }
        }

        public string Diff()
        {
            StringBuilder result = new StringBuilder();
            foreach (MonitoredProperty monitoredProperty in this)
            {
                if(monitoredProperty.HasChanged() == true)
                {
                    result.AppendLine(monitoredProperty.ToString());
                }
            }
            return result.ToString();
        }
    }
}
