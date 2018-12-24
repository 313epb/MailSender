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

        /// <summary>
        /// Все получатели писем в этой рассылке
        /// </summary>
        public virtual ICollection<Receiver> Receievers{get;set;}

        /// <summary>
        /// Почта, с которой отправлялось
        /// </summary>
        public Sender Sender { get; set; }

        /// <summary>
        /// SMTP сервер отправитель
        /// </summary>
        public SMTP SMTP { get; set; }

        /// <summary>
        /// Отправленное письмо
        /// </summary>
        public Mail Mail { get; set; }

        public string Name => Mail.Topic + " " + Sender.Email + " " + Created;
    }
}