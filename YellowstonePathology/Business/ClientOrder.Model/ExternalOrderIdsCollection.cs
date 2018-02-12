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
    public class ExternalOrderIdsCollection : ObservableCollection<ExternalOrderIds>
    {
        public ExternalOrderIdsCollection() { }

        public ExternalOrderIdsCollection(ClientOrderCollection clientOrders)
        {
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in clientOrders)
            {
                if (string.IsNullOrEmpty(clientOrder.ExternalOrderId) == false)
                {
                    ExternalOrderIds accessionOrderIds = new Model.ExternalOrderIds(clientOrder);
                    this.Add(accessionOrderIds);
                }
            }
        }

        public static ExternalOrderIdsCollection FromJSONstring(string jString)
        {
            ExternalOrderIdsCollection result = new Model.ExternalOrderIdsCollection();
            if (string.IsNullOrEmpty(jString) == false)
            {
                if (jString[0] == '[')
                {
                    result = JsonConvert.DeserializeObject<ExternalOrderIdsCollection>(jString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                }
            }

            return result;
        }

        public string ToJSONString()
        {
            string result = JsonConvert.SerializeObject(this);
            return result;
        }


        public bool Exists(int panelSetId)
        {
            bool result = false;
            foreach (ExternalOrderIds accessionOrderIds in this)
            {
                if (accessionOrderIds.PanelSetId == panelSetId)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        public string GetExternalOrderId(int panelSetId)
        {
            string result = null;
            foreach (ExternalOrderIds accessionOrderIds in this)
            {
                if (accessionOrderIds.PanelSetId == panelSetId)
                {
                    result = accessionOrderIds.ExternalOrderId;
                    break;
                }
            }

            return result;
        }
    }
}
