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
    public class SMTP: PairEntity
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

        public override string Error { get; }

        public virtual string this[string columnName] 
{
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Key":
                        Regex reg = new Regex("^[a-zA-Z0-9.!£#$%&'^_`{}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$");
                        if ((Key==null)||(!reg.IsMatch(Key))) error+="Введите корректный непустой адресс SMTP.";
                        break;
                    case "Value":
                        if (!string.IsNullOrEmpty(Value))
                        {
                            int port;
                            bool res;
                            res = Int32.TryParse(Value, out port);
                            if (!res) error = "Можно вводить только числа.";
                            else if (port < 100 || port > 999)
                                error = "Корректный порт находится в диапазоне от 100 до 1000.";
                        }
                        else
                        {
                            error = "Номер порта должен быть определён.";
                        }
                        break;
                }
                return error;
            }
        }
    }
}