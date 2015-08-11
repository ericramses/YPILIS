using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.YpiConnect.Contract
{
    [DataContract]
    public class Message
    {
        private string m_From;
        private string m_To;
        private string m_Subject;
        private string m_Text;
        private string m_PatientName;
        private string m_ReportNo;        

        public Message()
        {

        }

        public Message(string sendTo, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webSeviceAccount)
        {            
            this.m_From = "Webservice.Guest@ypii.com";
            this.m_To = sendTo;
			this.Subject = "Message From: " + webSeviceAccount.DisplayName;
			if (string.IsNullOrEmpty(webSeviceAccount.Client.Telephone) == false)
			{
				this.Subject += " - " + webSeviceAccount.Client.Telephone.FormattedTelephoneNumber();
			}
        }

		public string GetMessageBody()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(this.ReportNo))
			{
				stringBuilder.AppendLine(this.ReportNo);
			}
			if (!string.IsNullOrEmpty(this.PatientName))
			{
				stringBuilder.AppendLine("Patient Name:  " + this.PatientName);
			}
			if (!string.IsNullOrEmpty(this.ClientText))
			{
				stringBuilder.Append(this.ClientText);
			}

			return stringBuilder.ToString();
		}

        [DataMember]
        public string From
        {
            get { return this.m_From; }
            set { this.m_From = value; }
        }

        [DataMember]
        public string Subject
        {
            get { return this.m_Subject; }
            set { this.m_Subject = value; }
        }

        [DataMember]
        public string To
        {
            get { return this.m_To; }
            set { this.m_To = value; }
        }

        [DataMember]
        public string ClientText
        {
            get { return this.m_Text; }
            set { this.m_Text = value; }
        }

        [DataMember]
        public string PatientName
        {
            get { return this.m_PatientName; }
            set { this.m_PatientName = value; }
        }

        [DataMember]
        public string ReportNo
        {
            get {return this.m_ReportNo;}
            set { this.m_ReportNo = value;}
        }       
    }
}
