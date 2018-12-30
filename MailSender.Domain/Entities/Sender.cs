using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{

    /// <summary>
    /// Класс отправителя
    /// </summary>
    public class Sender: PairEntity//,IDataErrorInfo
    {
        /// <summary>
        /// Название класса
        /// </summary>
        public override string ClassName
        {
            get => Constants.ClassNamesConstants.SenderClassName;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public override string Key { get; set; }
        public override string KeyName { get=>Constants.ClassNamesConstants.SenderKeyName; }

        public override string Value { get; set; }
        public override string ValueName { get=>Constants.ClassNamesConstants.SenderValueName; }

        public static Sender ConvertFromIPair(IPair item)
        {
            return new Sender
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
        //            Regex reg = new Regex("^[-._a-z0-9]+@(?:[a-z0-9][-a-z0-9]+\\.)+[a-z]{2,6}$");
        //            if (!reg.IsMatch(Convert.ToString(Key)))
        //            {
        //                return "Введите корректный почтовый адрес";
        //            }
        //        }

        //        if (columnName == Value)
        //        {
        //            if (Value.Length < 6) return "Пароль не может быть короче 6 символов";
        //        }

        //        return "";
        //    }
        //}
    }
}