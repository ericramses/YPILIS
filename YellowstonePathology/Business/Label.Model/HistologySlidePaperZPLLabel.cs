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

        public static string GetCommands(string slideOrderId, string reportNo, string lastName, string testName, string slideLabel, string location)
        {
            StringBuilder result = new StringBuilder();
            result.Append("^XA");

            result.Append("^FO" + 28 + ",090^BXN,04,200^FD" + slideOrderId + "^FS");
            result.Append("^FO" + 28 + ",030^ATN,40,40^FD" + reportNo + "^FS");
            result.Append("^FO" + 28 + ",180^ARN,25,25^FD" + lastName + "^FS");
            result.Append("^FO" + 28 + ",210^ARN,25,25^FD" + testName + "^FS");
            result.Append("^FO" + 140 + ",118^ATN,25,25^FD" + slideLabel + "^FS");
            result.Append("^FO" + 100 + ",240^AQN,25,25^FD" + location + "^FS");

            result.Append("^XZ");
            return result.ToString();
        }        
    }
}
