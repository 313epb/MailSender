using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MailSender.Domain.Entities.Base;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Данные об отправленных письмах
    /// </summary>
    public class Sended : DateTimeEntity
    {
        //public Sended()
        //{
        //    Name= Mail.Topic + " " + Sender.Key + " " + Created;
        //}
        /// <summary>
        /// Все получатели писем в этой рассылке
        /// </summary>
        //public ICollection<Receiver> Receivers { get; set; }

        public virtual ICollection<SendedReceiver> SendedReceivers { get;set;}

        //public Sended()
        //{
        //    SendedReceivers= new ObservableCollection<SendedReceiver>();
        //}

        /// <summary>
        /// Почта, с которой отправлялось
        /// </summary>
        public Sender Sender { get; set; }

        public override DateTime Created { get; set; }

        /// <summary>
        /// SMTP сервер отправитель
        /// </summary>
        public SMTP SMTP { get; set; }

        /// <summary>
        /// Отправленное письмо
        /// </summary>
        public Mail Mail { get; set; }

        public string Name => Mail?.Topic + " " + Sender?.Key + " " + Created;
    }
}