using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class AliquotPaperZPLLabel
    {
        private string m_MasterAccessionNo;
        private string m_AliquotOrderId;
        private string m_LastName;
        private string m_FirstName;
        private string m_AliquotId;

        public AliquotPaperZPLLabel(string aliquotOrderId, string lastName, string firstName, string aliquotId, string masterAccessionNo)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_LastName = lastName;
            this.m_FirstName = firstName;
            this.m_AliquotOrderId = aliquotOrderId;
            this.m_AliquotId = aliquotId;
        }


        public void AppendCommands(StringBuilder zplString, int xOffset)
        {
            string truncatedFirstName = null;
            if (this.m_FirstName.Length > 13)
            {
                truncatedFirstName = this.m_FirstName.Substring(0, 13);
            }
            else
            {
                truncatedFirstName = this.m_FirstName;
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

            zplString.Append("^FO" + (30 + xOffset) + ",090^BXN,08,200^FD" + this.m_AliquotOrderId + "^FS");
            zplString.Append("^FO" + (30 + xOffset) + ",040^ATN,40,40^FD" + this.m_MasterAccessionNo + "^FS");
            zplString.Append("^FO" + (30 + xOffset) + ",220^AQN,25,25^FD" + truncatedLastName + "^FS");
            zplString.Append("^FO" + (30 + xOffset) + ",245^AQN,25,25^FD" + truncatedFirstName + "^FS");
            zplString.Append("^FWB^FO" + (150 + xOffset) + ",135^AT,25,25^FD" + this.m_AliquotId + "^FS");
            zplString.Append("^FO" + (195 + xOffset) + ",140^AQ,25,25^FD" + "YPI" + "^FS");
            zplString.Append("^FO" + (220 + xOffset) + ",120^AR,25,25^FD" + "6/8/17" + "^FS");
        }        
    }
}
