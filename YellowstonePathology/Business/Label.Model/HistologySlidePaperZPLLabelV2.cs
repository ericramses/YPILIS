using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class HistologySlidePaperZPLLabelV2 : ZPLCommand
    {
        private string m_SlideOrderId;
        private string m_ReportNo;
        private string m_LastName;
        private string m_FirstName;      
        private string m_TestName;
        private string m_SlideLabel;
        private string m_Location;        
        private bool m_UseWetProtocol;
        private bool m_PerformedByHand;

        public HistologySlidePaperZPLLabelV2(string slideOrderId, string reportNo, string firstName, string lastName, string testName, string slideLabel, string location, bool useWetProtocol, bool performedByHand)
        {
            this.m_SlideOrderId = slideOrderId;
            this.m_ReportNo = reportNo;
            this.m_LastName = lastName;
            this.m_FirstName = firstName;         
            this.m_TestName = testName;
            this.m_SlideLabel = slideLabel;
            this.m_Location = location;        
            this.m_UseWetProtocol = useWetProtocol;
            this.m_PerformedByHand = performedByHand;
        }

        public override string GetCommand()
        {
            StringBuilder result = new StringBuilder();
            string truncatedTestName = null;
            if (this.m_TestName.Length > 16)
            {
                truncatedTestName = this.m_TestName.Substring(0, 16);
            }
            else
            {
                truncatedTestName = this.m_TestName;
            }

            if (this.m_UseWetProtocol == true) truncatedTestName = truncatedTestName + "W";
            if (this.m_PerformedByHand == true) truncatedTestName = truncatedTestName + "H";

            string patientname = null;
            if (this.m_LastName.Length > 10)
            {
                patientname = this.m_LastName.Substring(0, 10);
            }
            else
            {
                patientname = this.m_LastName;
            }

            if(string.IsNullOrEmpty(this.m_FirstName) == false)
            {
                patientname = patientname + " " + this.m_FirstName.Substring(0, 1);
            }

            result.Append("~SD20");
            result.Append("^PW420");
            result.Append("^FWB");
            result.Append("^A0,35,35^FO0,0^FB335,1,,^FD" + this.m_SlideOrderId + "^FS");
            result.Append("^FO50,180^BXB,04,200^FD" + "HSLD" + this.m_SlideOrderId + "^FS");
            result.Append("^A0,25,25^FO150,0^FB330,1,,^FD" + patientname + "^FS");
            
            result.Append("^A0N,35,35^FO5,60^FD" + truncatedTestName + "^FS");            
            //result.Append("^A0N,30,30^FO120,0^FB330,1,,^FD" + patientname + "^FS");    
            return result.ToString();        
        }
    }
}
