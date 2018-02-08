using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class AccessionOrderIdsCollection : ObservableCollection<AccessionOrderIds>
    {
        public AccessionOrderIdsCollection() { }

        public AccessionOrderIdsCollection(ClientOrderCollection clientOrders)
        {
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in clientOrders)
            {
                AccessionOrderIds accessionOrderIds = new Model.AccessionOrderIds(clientOrder);
                this.Add(accessionOrderIds);
            }
        }

        public static AccessionOrderIdsCollection FromJSONstring(string jString)
        {
            AccessionOrderIdsCollection result = JsonConvert.DeserializeObject<AccessionOrderIdsCollection>(jString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });

            return result;
        }

        public string ToJSONString()
        {
            string result = JsonConvert.SerializeObject(this);
            return result;
        }
    }
}
