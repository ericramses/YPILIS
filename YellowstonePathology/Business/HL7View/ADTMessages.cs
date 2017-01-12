using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.HL7View
{
    public class ADTMessages
    {
        ObservableCollection<ADTMessage> m_Messages;         

        public ADTMessages()
        {
            this.m_Messages = new ObservableCollection<ADTMessage>();            
        }          
        
        public ObservableCollection<ADTMessage> Messages
        {
            get { return this.m_Messages; }
        }

        public Business.Patient.Model.Address GetPatientAddress()
        {
            Business.Patient.Model.Address result = null;
            foreach(ADTMessage message in this.m_Messages)
            {
                if(string.IsNullOrEmpty(message.PatientAddress.PAddress1) == false)
                {
                    result = message.PatientAddress;
                    break;
                }
            }
            return result;
        }

        public List<IN1> GetUniqueIN1Segments()
        {
            List<IN1> result = new List<IN1>();
            foreach (ADTMessage adtMessage in this.m_Messages)
            {
                foreach(Business.HL7View.IN1 in1 in adtMessage.IN1Segments)
                {
                    if(string.IsNullOrEmpty(in1.InsuranceName) == false)
                    {
                        if(!result.Exists(item => item.InsuranceName == in1.InsuranceName))
                        {
                            result.Add(in1);
                        }
                    }
                }
            }
            return result;
        }        
    }
}
