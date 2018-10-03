using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Billing.Model
{
    public class SVHClinicMailMessage
    {
        public SVHClinicMailMessage()
        {

        }

        public static void SendMessage(DateTime accessionDate)
        {
            string messageBody = Business.Gateway.AccessionOrderGateway.GetSVHClinicMessageBody(accessionDate);
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("support@ypii.com", "cheryl.stoltz@sclhealth.org", System.Windows.Forms.SystemInformation.UserName, messageBody);
            message.To.Add("rebecca.ricci@sclhealth.org");
            message.To.Add("sid.harder@ypii.com");
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(message);
        }
    }
}
