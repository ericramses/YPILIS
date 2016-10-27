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
        private string m_PatientInitials;
        private string m_BlockId;

        public HistologyBlockPaperZPLLabel(string aliquotOrderId, string patientInitials, string blockId, string masterAccessionNo)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_PatientInitials = patientInitials;
            this.m_AliquotOrderId = aliquotOrderId;
            this.m_BlockId = blockId;
        }

        public void AppendCommands(StringBuilder zplString, int xOffset)
        {
            zplString.Append("^FO" + (30 + xOffset) + ",090^BXN,08,200^FD" + "HBLK" + this.m_AliquotOrderId + "^FS");
            zplString.Append("^FO" + (30 + xOffset) + ",040^ATN,40,40^FD" + this.m_MasterAccessionNo + "^FS");
            zplString.Append("^FO" + (30 + xOffset) + ",220^ARN,25,25^FD" + this.m_PatientInitials + "^FS");
            zplString.Append("^FO" + (175 + xOffset) + ",220^ARN,25,25^FD" + this.m_BlockId + "^FS");            
        }        
    }
}
