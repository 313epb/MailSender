using System;
using System.Windows;
using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{

    /// <summary>
    /// Класс для писем
    /// </summary>
    public class Mail:DateTimeEntity
    {

        /// <summary>
        /// Содержание 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Тип письма
        /// </summary>
        public bool IsHTML { get; set; }


        /// <summary>
        /// Тема 
        /// </summary>
        public string Topic { get; set; }

        public override DateTime Created { get=>DateTime.Now; }
    }
}