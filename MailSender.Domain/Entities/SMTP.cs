using System;
using System.Net.Mail;
using MailSender.Domain.Constants;
using MailSender.Domain.Entities.Base;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс для SMTP серверов
    /// </summary>
    public class SMTP: PairEntity
    {
        public override string ClassName => ClassNamesConstants.SMTPClassName;

        public override string Key { get; set; }
        public override string KeyName { get=>ClassNamesConstants.SMTPKeyName; }

        public override string Value { get; set; }
        public override string ValueName { get=>ClassNamesConstants.SMTPValueName; }

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