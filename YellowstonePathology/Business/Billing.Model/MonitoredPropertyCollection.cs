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
        public void Load(object o)
        {            
            PropertyInfo[] properties = o.GetType().GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.MonitorProperty))).ToArray();
            foreach (PropertyInfo property in properties)
            {
                MonitoredProperty monitoredProperty = new MonitoredProperty(property, o);
            }
        }

        public void Diff()
        {
            foreach (MonitoredProperty monitoredProperty in this)
            {
                //if(moni)
            }
        }
    }
}
