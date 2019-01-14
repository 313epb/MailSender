using System;
using System.ComponentModel;
using System.Windows;
using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{

    /// <summary>
    /// Класс для писем
    /// </summary>
    public class Mail:DateTimeEntity,IDataErrorInfo
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

        public override DateTime Created { get; set; }

        #region Валидация

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Topic":
                        if (string.IsNullOrEmpty(Topic))
                        {
                            error = "Тема письма должна быть заполнена.";
                        }
                       break;
                }
                return error;
            }
        }

        #endregion
    }
}