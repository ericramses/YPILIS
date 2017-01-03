using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.HL7View
{
    public class GT1
    {
        private string m_GuarantorName;
        private string m_GuarantorAddress;
        
        public GT1()
        {

        }

        public void FromHL7(string in1Segment)
        {
            string[] split = in1Segment.Split('|');
            this.m_GuarantorName = split[3];            

            if(string.IsNullOrEmpty(split[5]) == false)
            {
                string[] subAddressSubfields = split[5].Split('^');
                this.m_GuarantorAddress = subAddressSubfields[0] + ", " + subAddressSubfields[2] + ", " + subAddressSubfields[3] + ", " + subAddressSubfields[4];
            }                        
        }

        public string GuarantorName
        {
            get { return this.m_GuarantorName; }
            set {  this.m_GuarantorName = value; }
        }

        public string GuarantorAddress
        {
            get { return this.m_GuarantorAddress; }
            set { this.m_GuarantorAddress = value; }
        } 
        
        public string DisplayString
        {
            get
            {
                return this.m_GuarantorName + Environment.NewLine + this.m_GuarantorAddress;
            }
        }       
    }
}
