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
        private string m_PathologistInitials;

        public HistologySlidePaperZPLLabel(string slideOrderId, string reportNo, string lastName, string testName, string slideLabel, string location, string pathologistInitials)
        {
            this.m_SlideOrderId = slideOrderId;
            this.m_ReportNo = reportNo;
            this.m_LastName = lastName;            
            this.m_TestName = testName;
            this.m_SlideLabel = slideLabel;
            this.m_Location = location;
            this.m_PathologistInitials = pathologistInitials;        
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
            if (this.m_LastName.Length > 16)
            {
                truncatedLastName = this.m_LastName.Substring(0, 16);
            }
            else
            {
                truncatedLastName = this.m_LastName;
            }

            result.Append("^PW440");
            result.Append("^FWB");
            result.Append("^A0,30,30^FO10,60^FB210,1,,^FD" + this.m_ReportNo + "^FS");
            result.Append("^A0,25,25^FO45,60^FB210,1,,^FD" + truncatedLastName + "^FS");
            result.Append("^A0,30,30^FO80,60^FB210,1,,^FD" + truncatedTestName + "^FS");
            string slideLabelInitials = this.m_SlideLabel + " (" + this.m_PathologistInitials + ")";
            result.Append("^A0,30,30^FO115,60^FB210,1,,^FD" + slideLabelInitials + "^FS");
            result.Append("^A0,20,20^FO160,60^FB210,1,,^FD" + this.m_Location + "^FS");
            result.Append("^FO120,20^BXR,04,200^FD" + "HSLD" + this.m_SlideOrderId + "^FS");              
        }
    }
}
