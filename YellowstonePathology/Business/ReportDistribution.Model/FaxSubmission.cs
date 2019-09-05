using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class FaxSubmission
    {        
        public static DistributionResult Submit(string faxNumber, string subject, string fileName)
        {
            DistributionResult result = new DistributionResult();

            if (System.IO.File.Exists(fileName) == false)
            {
                result.Message = "Not able to send fax because the file does not exist: " + fileName;
                result.IsComplete = false;
                return result;
            }            

            FAXCOMEXLib.FaxServer faxServer = new FAXCOMEXLib.FaxServer();
            faxServer.Connect("ypiifax");

            FAXCOMEXLib.FaxDocument faxDoc = new FAXCOMEXLib.FaxDocument();
            faxDoc.Body = fileName;

            Business.LocalPhonePrefix localPhonePrefix = new LocalPhonePrefix();
            faxNumber = localPhonePrefix.HandleLongDistance(faxNumber);            

            faxDoc.Recipients.Add(faxNumber, subject);
            faxDoc.DocumentName = subject;
            faxDoc.Sender.Company = "YPII";
            faxDoc.Subject = subject;            

            faxDoc.Priority = FAXCOMEXLib.FAX_PRIORITY_TYPE_ENUM.fptLOW;            
            faxDoc.ConnectedSubmit(faxServer);            
            faxServer.Disconnect();

            result.IsComplete = true;
            return result;
        }        
    }
}
