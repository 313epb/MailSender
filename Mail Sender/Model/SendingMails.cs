using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MailSender.Domain.Entities;

namespace Mail_Sender.Model
{
    class SendingMails
    {
        public async static void Send(Sended item)
        {
            foreach (SendedReceiver itemSR in item.SendedReceivers)
            {
                await SendMailsAsync(item, itemSR.Receiver);
            }
        }

        private static Task SendMailsAsync(Sended item, Receiver itemSR)
        {
            MailMessage mm = new MailMessage(item.Sender.Key, itemSR.Key);
            mm.Subject = item.Mail.Topic;
            mm.Body = item.Mail.Content;
            mm.IsBodyHtml = item.Mail.IsHTML;
            SmtpClient sc = new SmtpClient(item.SMTP.Key, Int32.Parse(item.SMTP.Value));
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential(item.Sender.Key, item.Sender.Value);
            try
            {
                return Task.Run(()=>sc.Send(mm));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
