using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class HistologySlidePaperZPLLabel
    {
        public HistologySlidePaperZPLLabel()
        {

        }

        public static string GetCommands(string slideOrderId, string reportNo, string lastName, string testname, string slideLabel, string location)
        {
            StringBuilder result = new StringBuilder();
            result.Append("^XA");        
            
            //result.Append("^FO" + 30 + ",090^BXN,08,200^FD" + histologyBlock.AliquotOrderId + "^FS");
            //result.Append("^FO" + 30 + ",040^ATN,40,40^FD" + masterAccessionNo + "^FS");
            //result.Append("^FO" + 30 + ",220^ARN,25,25^FD" + patientInitials + "^FS");
            //result.Append("^FO" + 175 + ",220^ARN,25,25^FD" + blockId + "^FS");

            result.Append("^XZ");
            return result.ToString();
        }        
    }
}
