using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ExternalOrderIds(string formattedValue)
        {
            string[] values = formattedValue.Split(new char[] { ',' });
            this.m_ExternalOrderId = values[0];
            this.m_PanelSetId = Convert.ToInt32(values[1]);
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

        public string FormattedValue
        {
            get { return this.m_ExternalOrderId + "," + this.m_PanelSetId.ToString(); }
        }
    }
}
