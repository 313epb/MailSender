using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MailSender.Domain.Entities;

namespace Mail_Sender.Model
{
    internal class SendingMails
    {
        private  static Dictionary<SendedReceiver, string> _errDic = new Dictionary<SendedReceiver, string>();

        public delegate void SendedHandler(Dictionary<SendedReceiver, string> errDictionary, Sended item);

        public static event SendedHandler AllSended;

        private static void ClearErrDic()
        {
            _errDic = new Dictionary<SendedReceiver, string>();
        }

        public static async void Send(Sended item)
        {
            foreach (SendedReceiver itemSR in item.SendedReceivers)
            {
                await SendMailsAsync(item, itemSR);
            }

            AllSended?.Invoke(_errDic, item);

            ClearErrDic();
        }

        private static async Task SendMailsAsync(Sended item, SendedReceiver itemSR)
        {
            MailMessage mm = new MailMessage(item.Sender.Key, itemSR.Receiver.Key);
            mm.Subject = item.Mail.Topic;
            mm.Body = item.Mail.Content;
            mm.IsBodyHtml = item.Mail.IsHTML;
            SmtpClient sc = new SmtpClient(item.SMTP.Key, int.Parse(item.SMTP.Value))
            {
                EnableSsl = true, Credentials = new NetworkCredential(item.Sender.Key, item.Sender.Value)
            };
            try
            {
                await Task.Run(()=>sc.Send(mm));
            }
            catch (Exception e)
            {
                _errDic.Add(itemSR, e.Message);
            }
        }
    }
}
