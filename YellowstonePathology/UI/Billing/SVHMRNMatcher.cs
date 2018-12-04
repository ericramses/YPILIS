using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Billing
{
    public class SVHMRNMatcher
    {
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.ClientOrder.Model.ClientOrder m_ClientOrder;

        private string m_ANumber;
        private string m_VNumber;
        private string m_VNumberAccount;
        private bool m_MatchFound;

        public SVHMRNMatcher(Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public Business.ClientOrder.Model.ClientOrder ClientOrder
        {
            get { return this.m_ClientOrder; }
        }

        public void Match()
        {
            if(string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == false)
            {
                if (this.m_AccessionOrder.SvhMedicalRecord.StartsWith("A") == true)
                {
                    this.m_ANumber = this.m_AccessionOrder.SvhMedicalRecord;
                    Business.ClientOrder.Model.ClientOrderCollection clientOrders = Business.Gateway.ClientOrderGateway.GetClientOrdersByANumber(this.m_AccessionOrder.SvhMedicalRecord);
                    if(clientOrders.Count > 0)
                    {
                        if(this.DoesPatientNameMatch(this.m_AccessionOrder, clientOrders[0]) == true)
                        {
                            this.m_ClientOrder = clientOrders[0];
                            this.m_VNumber = clientOrders[0].SvhMedicalRecord;
                            this.m_VNumberAccount = clientOrders[0].SvhAccountNo;
                            this.m_MatchFound = true;
                        }
                        else
                        {
                            throw new Exception("Patient name in client order and accession order don't match.");
                        }                       
                    }
                    else
                    {
                        this.m_VNumber = null;
                        this.m_MatchFound = false;
                    }
                }
                else
                {
                    this.m_VNumber = this.m_AccessionOrder.SvhMedicalRecord;
                    this.m_ANumber = null;
                    this.m_MatchFound = false;
                }
            }
            else
            {
                this.m_ANumber = null;
                this.m_MatchFound = false;
            }            
        }    
        
        public bool DoesPatientNameMatch(Business.Test.AccessionOrder ao, Business.ClientOrder.Model.ClientOrder co)
        {
            bool result = true;
            if (ao.PLastName.ToUpper() != co.PLastName.ToUpper()) result = false;            
            return result;
        }    

        public string ANumber
        {
            get { return this.m_ANumber; }
            set { this.m_ANumber = value; }
        }

        public string VNumber
        {
            get { return this.m_VNumber; }
            set { this.m_VNumber = value; }
        }

        public string VNumberAccount
        {
            get { return this.m_VNumberAccount; }
            set { this.m_VNumberAccount = value; }
        }

        public bool MatchFound
        {
            get { return this.m_MatchFound; }
            set { this.m_MatchFound = value; }
        }
    }
}
