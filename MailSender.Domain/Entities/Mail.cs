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
    public class Mail:DateTimeEntity//,IDataErrorInfo
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
        //public string Error { get=>""; }

        //public string this[string columnName]
        //{
        //    get
        //    {
        //        if (columnName == "Topic")
        //            if (Topic.Length == 0)
        //                return "Тема не должна быть пустой";
        //        return "";
        //    }
        //}
    }
}