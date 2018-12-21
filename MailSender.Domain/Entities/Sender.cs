﻿using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{

    /// <summary>
    /// Класс отправителя
    /// </summary>
    public class Sender:NamedEntity,IPair
    {
        /// <summary>
        /// Название класса
        /// </summary>
        public static string ClassName
        {
            get => Constants.ClassNamesConstants.SenderClassName;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Key { get=>Email; set=>Email=value; }
        public string Value { get=>Password; set=>Password=value; }
    }
}