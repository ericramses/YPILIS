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

        public void ParseHL7()
        {
            /*
            this.m_PFirstName = this.m_Messages[0].PFirstName;
            this.m_PLastName = this.m_Messages[0].PLastName;
            this.m_PBirthdate = this.m_Messages[0].PBirthdate;               

            foreach(ADTMessage message in this.m_Messages)
            {
                string [] lines = message.Message.Split('\r');
                for(int i=0; i<lines.Length; i++)
                {
                    string[] fields = lines[i].Split('|');
                    if(fields[0] == "IN1")
                    {
                        Business.HL7View.IN1 in1 = new HL7View.IN1();
                        in1.FromHl7(lines[i]);
                        this.m_IN1Segments.Add(in1);
                    }

                    if(fields[0] == "GT1")
                    {
                        Business.HL7View.GT1 gt1 = new HL7View.GT1();
                        gt1.FromHl7(lines[i]);
                        this.m_Guarantor = gt1.GuarantorName + Environment.NewLine + gt1.GuarantorAddress;
                    }
                }
            }
            */            
        }
    }
}
