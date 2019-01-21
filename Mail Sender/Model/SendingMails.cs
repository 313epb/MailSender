using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;
using MailSender.Domain.Entities;

namespace Mail_Sender.Model
{
    class SendingMails
    {
        private  static Dictionary<SendedReceiver, string> errDic = new Dictionary<SendedReceiver, string>();

        public delegate void SendedHandler(Dictionary<SendedReceiver, string> errDictionary, Sended item);

        public static event SendedHandler AllSended;

        private static void ClearErrDic()
        {
            errDic = new Dictionary<SendedReceiver, string>();
        }

        public async static void Send(Sended item)
        {
            foreach (SendedReceiver itemSR in item.SendedReceivers)
            {
                await SendMailsAsync(item, itemSR);
            }

            AllSended?.Invoke(errDic, item);

            ClearErrDic();
        }

        private static async Task SendMailsAsync(Sended item, SendedReceiver itemSR)
        {
            MailMessage mm = new MailMessage(item.Sender.Key, itemSR.Receiver.Key);
            mm.Subject = item.Mail.Topic;
            mm.Body = item.Mail.Content;
            mm.IsBodyHtml = item.Mail.IsHTML;
            SmtpClient sc = new SmtpClient(item.SMTP.Key, Int32.Parse(item.SMTP.Value));
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential(item.Sender.Key, item.Sender.Value);
            try
            {
                await Task.Run(()=>sc.Send(mm));
            }
            catch (Exception e)
            {
                errDic.Add(itemSR, e.Message);
            }
        }
    }
}
