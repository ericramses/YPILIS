using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YellowstonePathology.UI.AppMessaging
{
    public class AccessionLockMessage
    {        
        private string m_MasterAccessionNo;
        private string m_ComputerName;
        private string m_UserName;
        private AccessionLockMessageIdEnum m_MessageId;
        private string m_Message;

        public AccessionLockMessage(string masterAccessionNo, string computerName, string userName, AccessionLockMessageIdEnum messageId)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_ComputerName = computerName;
            this.m_UserName = userName;
            this.m_MessageId = messageId;

            switch(messageId)
            {
                case AccessionLockMessageIdEnum.ASK:
                    this.m_Message = "Give me the case.";
                    break;
                case AccessionLockMessageIdEnum.GIVE:
                    this.m_Message = "OK you can have it.";
                    break;
                case AccessionLockMessageIdEnum.HOLD:
                    this.m_Message = "Hold your horses!";
                    break;
            }
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
        }

        public string ComputerName
        {
            get { return this.m_ComputerName; }
        }

        public string UserName
        {
            get { return this.m_UserName; }
        }

        public AccessionLockMessageIdEnum MessageId
        {
            get { return this.m_MessageId; }
        }

        public string Message
        {
            get { return this.m_Message; }
        }        

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
        
        public bool DidISendThis()
        {
            bool result = false;
            if(this.m_ComputerName == System.Environment.MachineName)
            {
                result = true;
            }
            return result;
        }        
    }
}
