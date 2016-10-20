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

        public HistologySlidePaperZPLLabel(string slideOrderId, string reportNo, string lastName, string testName, string slideLabel, string location)
        {
            this.m_SlideOrderId = slideOrderId;
            this.m_ReportNo = reportNo;
            this.m_LastName = lastName;
            this.m_TestName = testName;
            this.m_SlideLabel = slideLabel;
            this.m_Location = location;
        }

        public void AppendCommands(StringBuilder result, int xOffset)
        {                        
            string truncatedtestName = null;
            if (this.m_TestName.Length > 13)
            {
                truncatedtestName = this.m_TestName.Substring(0, 13);
            }
            else
            {
                truncatedtestName = this.m_TestName;
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
                 
            result.Append("^FO" + (28 + xOffset) + ",090^BXN,04,200^FD" + this.m_SlideOrderId + "^FS");
            result.Append("^FO" + (28 + xOffset) + ",030^ATN,40,40^FD" + this.m_ReportNo + "^FS");
            result.Append("^FO" + (28 + xOffset) + ",180^ARN,25,25^FD" + truncatedLastName + "^FS");
            result.Append("^FO" + (28 + xOffset) + ",210^ARN,25,25^FD" + truncatedtestName + "^FS");
            result.Append("^FO" + (140 + xOffset) + ",118^ATN,25,25^FD" + this.m_SlideLabel + "^FS");
            result.Append("^FO" + (100 + xOffset) + ",240^AQN,25,25^FD" + this.m_Location + "^FS");            
        }        
    }
}
