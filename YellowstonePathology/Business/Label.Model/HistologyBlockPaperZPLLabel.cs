using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class HistologyBlockPaperZPLLabel
    {
        private string m_MasterAccessionNo;
        private string m_AliquotOrderId;
        private string m_LastName;
        private string m_BlockId;

        public HistologyBlockPaperZPLLabel(string aliquotOrderId, string lastName, string blockId, string masterAccessionNo)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_LastName = lastName;
            this.m_AliquotOrderId = aliquotOrderId;
            this.m_BlockId = blockId;
        }


        public void AppendCommands(StringBuilder zplString, int xOffset)
        { 

        string truncatedLastName = null;
        if (this.m_LastName.Length > 13)
        {
            truncatedLastName = this.m_LastName.Substring(0, 13);
        }
        else
        {
           truncatedLastName = this.m_LastName;
        }
        
            zplString.Append("^FO" + (30 + xOffset) + ",090^BXN,08,200^FD" + "HBLK" + this.m_AliquotOrderId + "^FS");
            zplString.Append("^FO" + (30 + xOffset) + ",040^ATN,40,40^FD" + this.m_MasterAccessionNo + "^FS");
            zplString.Append("^FO" + (30 + xOffset) + ",220^ARN,25,25^FD" + truncatedLastName + "^FS");
            zplString.Append("^FO" + (175 + xOffset) + ",130^ATN,25,25^FD" + this.m_BlockId + "^FS");
            zplString.Append("^FO" + (30 + xOffset) + ",250^AQN,25,25^FD" + "YPI Billings" + "^FS");
        }        
    }
}
