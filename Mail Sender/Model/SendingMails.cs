using System;
using System.Net;
using System.Net.Mail;
using MailSender.Domain.Entities;

namespace Mail_Sender.Model
{
    class SendingMails
    {
        public static void Send(Sended item)
        {
            foreach (Receiver itemReceiever in item.Receievers)
            {
                using (MailMessage mm = new MailMessage(item.Sender.Email, itemReceiever.Email))
                {
                    mm.Subject = item.Mail.Topic;
                    mm.Body = item.Mail.Content;
                    mm.IsBodyHtml = item.Mail.IsHTML;
                    using (SmtpClient sc = new SmtpClient(item.SMTP.SMTPName, Int32.Parse(item.SMTP.Port)))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential(item.Sender.Email, item.Sender.Password);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }
            }
        }
    }
}
