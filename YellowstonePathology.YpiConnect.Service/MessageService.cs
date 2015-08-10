using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace YellowstonePathology.YpiConnect.Service
{
    public class MessageService : YellowstonePathology.YpiConnect.Contract.IMessageService
    {
        public bool Ping()
        {
            return true;
        }

        public void Send(YellowstonePathology.YpiConnect.Contract.Message message)
        {                        
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(message.From, message.To, message.Subject, message.GetMessageBody());
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");
            client.Credentials = new System.Net.NetworkCredential("Administrator", "p0046e");
            client.Send(mailMessage);
        }        
    }
}
