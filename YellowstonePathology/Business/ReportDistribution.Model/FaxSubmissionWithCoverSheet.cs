using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class FaxSubmissionWithCoverSheet
    {
        public static DistributionResult Submit(string faxNumber, string subject, string fileName, DateTime reportDate)
        {
            DistributionResult result = new DistributionResult();

            FAXCOMEXLib.FaxServer faxServer = new FAXCOMEXLib.FaxServer();
            faxServer.Connect("ypiifax");

            FAXCOMEXLib.FaxDocument faxDoc = new FAXCOMEXLib.FaxDocument();
            faxDoc.Body = fileName;
            faxDoc.CoverPage = "Attention: Cari Williams" + Environment.NewLine + Environment.NewLine + "Yellowstone Pathology Institute" + Environment.NewLine + "DOH reports for " + reportDate.ToShortDateString();

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
