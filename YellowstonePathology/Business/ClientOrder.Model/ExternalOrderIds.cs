using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class ExternalOrderIds
    {
        private string m_ExternalOrderId;
        private int m_PanelSetId;

        public ExternalOrderIds() { }

        public ExternalOrderIds(ClientOrder clientOrder)
        {
            this.m_PanelSetId = clientOrder.PanelSetId.Value;
            this.m_ExternalOrderId = clientOrder.ExternalOrderId;
        }

        public string ExternalOrderId
        {
            get { return this.m_ExternalOrderId; }
            set { this.m_ExternalOrderId = value; }
        }

        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set { this.m_PanelSetId = value; }
        }
    }
}
