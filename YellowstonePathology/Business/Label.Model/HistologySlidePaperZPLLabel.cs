using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class HistologySlidePaperZPLLabel
    {
        private string m_SlideOrderId;
        private string m_ReportNo;
        private string m_LastName;        
        private string m_TestName;
        private string m_SlideLabel;
        private string m_Location;
        private string m_OrderedByInitials;

        public HistologySlidePaperZPLLabel(string slideOrderId, string reportNo, string lastName, string testName, string slideLabel, string location, string orderedByInitials)
        {
            this.m_SlideOrderId = slideOrderId;
            this.m_ReportNo = reportNo;
            this.m_LastName = lastName;            
            this.m_TestName = testName;
            this.m_SlideLabel = slideLabel;
            this.m_Location = location;
            this.m_OrderedByInitials = orderedByInitials;        
        }

        public void AppendCommands(StringBuilder result, int xOffset)
        {                        
            string truncatedTestName = null;
            if (this.m_TestName.Length > 16)
            {
                truncatedTestName = this.m_TestName.Substring(0, 16);
            }
            else
            {
                truncatedTestName = this.m_TestName;
            }

            string truncatedLastName = null;
            if (this.m_LastName.Length > 13)
            {
                truncatedLastName = this.m_LastName.Substring(0, 13);
            }
            else
            {
                truncatedLastName = this.m_LastName;
            }

            string slideLabelInitials = this.m_SlideLabel;
            if (this.m_OrderedByInitials != "NONE")
            {
                slideLabelInitials = " (" + this.m_OrderedByInitials + ")";
            }

            string reportNoDisplay = this.m_ReportNo.Substring(3);
            string yearDisplay = this.m_ReportNo.Substring(0, 2);

            result.Append("^PW440");
            result.Append("^FWB");
            result.Append("^FO10,60^BXB,04,200^FD" + "HSLD" + this.m_SlideOrderId + "^FS");
            result.Append("^A0,35,35^FO10,85^FB250,1,,^FD" + this.m_ReportNo + "^FS");
            result.Append("^A0,40,40^FO50,85^FB250,1,,^FD" + this.m_SlideLabel + "^FS");
            result.Append("^A0,35,35^FO95,85^FB250,1,,^FD" + truncatedTestName + "^FS");            
            result.Append("^A0,25,25^FO135,85^FB250,1,,^FD" + truncatedLastName + "^FS");
            result.Append("^A0,20,20^FO160,85^FB250,1,,^FD" + this.m_Location + "^FS");
            result.Append("^A0,35,35^FO155,60^FD" + this.m_OrderedByInitials + "^FS");
        }
    }
}
