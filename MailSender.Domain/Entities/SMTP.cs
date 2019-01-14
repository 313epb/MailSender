using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
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

        #region Валидация

        public override string Error { get; }

        public override string this[string columnName] 
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Key":
                        if (!string.IsNullOrEmpty(Key))
                        {
                            try
                            {
                                var smtpClient = new SmtpClient(Key);
                            }
                            catch (Exception e)
                            {
                                error = e.Message;
                            }
                        }
                        else
                        {
                            error = $"Введите корректный непустой {KeyName}.";
                        }
                        break;
                    case "Value":
                        if (!string.IsNullOrEmpty(Value))
                        {
                            int port;
                            bool res;
                            res = Int32.TryParse(Value, out port);
                            if (!res) error = "Можно вводить только числа.";
                            else if (port < 100 || port > 999)
                                error = $"Корректный {ValueName} находится в диапазоне от 100 до 1000.";
                        }
                        else
                        {
                            error = $"{ValueName} должен быть определён.";
                        }
                        break;
                }
                return error;
            }
        }

        #endregion
    }
}