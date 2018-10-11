using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Billing
{
    public class SVHMRNHandler
    {
        private Business.Test.AccessionOrder m_AccessionOrder;
        private string m_MRN;
        private bool m_MRNPresent;
        private bool m_IsANumber;

        public SVHMRNHandler(Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public void HandleMRN()
        {
            if(string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == false)
            {
                if (this.m_AccessionOrder.SvhMedicalRecord.StartsWith("A") == true)
                {
                    this.m_IsANumber = true;    
                    Business.ClientOrder.Model.ClientOrderCollection clientOrders = Business.Gateway.ClientOrderGateway.GetClientOrdersByANumber(this.m_AccessionOrder.SvhMedicalRecord);
                    if(clientOrders.Count > 0)
                    {
                        this.m_MRN = clientOrders[0].SvhMedicalRecord;
                        this.m_MRNPresent = true;
                    }
                    else
                    {
                        this.m_MRN = null;
                        this.m_MRNPresent = false;
                    }
                }
                else
                {
                    this.m_MRN = this.m_AccessionOrder.SvhMedicalRecord;
                    this.m_MRNPresent = true;
                }
            }
            else
            {
                this.m_MRN = null;
                this.m_MRNPresent = false;
            }
            
        }        

        public string MRN
        {
            get { return this.m_MRN; }
            set { this.m_MRN = value; }
        }

        public bool MRNPresent
        {
            get { return this.m_MRNPresent; }
            set { this.m_MRNPresent = value; }
        }

        public bool IsANumber
        {
            get { return this.m_IsANumber; }
            set { this.m_IsANumber = value; }
        }
    }
}
