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
            switch (clientOrder.OrderType)
            {
                case "Routine Surgical Pathology":
                    this.m_PanelSetId = 13;
                    break;
                case "CFYPI":
                    this.m_PanelSetId = 20;
                    break;
                case "THINPREP":
                    this.m_PanelSetId = 15;
                    break;
                default:
                    this.m_PanelSetId = 13;
                    break;
            }

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
