using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс для SMTP серверов
    /// </summary>
    public class SMTP: PairEntity//,IDataErrorInfo
    {
        public override string ClassName
        {
            get => Constants.ClassNamesConstants.SMTPClassName;
        }

        public  string SMTPName { get; set; }
        public  string Port { get; set; }

        public override string Key { get; set; }
        public override string KeyName { get=>Constants.ClassNamesConstants.SMTPKeyName; }

        public override string Value { get; set; }
        public override string ValueName { get=>Constants.ClassNamesConstants.SMTPValueName; }

        public static SMTP ConvertFromIPair(IPair item)
        {
            return new SMTP
            {
                Id = item.Id,
                Key = item.Key,
                Value = item.Value
            };
        }

        //public string Error { get => ""; }

        //public string this[string columnName]
        //{
        //    get
        //    {
        //        if (columnName == Key)
        //        {
        //            Regex reg = new Regex("^[a-zA-Z0-9.!£#$%&'^_`{}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$");
        //            if (!reg.IsMatch(Convert.ToString(Key)))
        //            {
        //                return "Введите корректный адрес SMTP";
        //            }
        //        }

        //        if (columnName == Value)
        //        {
        //            int port;
        //            try
        //            {
        //                port = Convert.ToInt32(Port);
        //            }
        //            catch (Exception e)
        //            {
        //                return  "Вводите только цифры";
        //            }

        //            if ((port < 100) && (port > 999)) return "Введите корректное значение порта";
        //        }

        //        return "";
        //    }
        //}
    }
}