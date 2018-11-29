using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Helpers
{
    public class EmailNotification
    {
        public static void SendNotification(string app)
        {
            MailAddress toAddress = new MailAddress("m.bublitz@shaw.ca");
            MailAddress fromAddress = new MailAddress("mike@mwbublitz.com");
            MailMessage message = new MailMessage(fromAddress, toAddress);

            message.Subject = "View Notificaiton";
            message.Body = string.Format("The {0} app has been viewed", app);

            SmtpClient mailServer = new SmtpClient
            {
                Host = "mail.mwbublitz.com",
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.ToString(), "star12@xbs")
            };
            mailServer.Send(message);
        }
    }
}
