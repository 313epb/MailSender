using System;
using System.Net.Mail;
using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{

    /// <summary>
    /// Класс отправителя
    /// </summary>
    public class Sender: PairEntity
    {
        /// <summary>
        /// Название класса
        /// </summary>
        public override string ClassName
        {
            get => Constants.ClassNamesConstants.SenderClassName;
        }


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
                                var mailAdress = new MailAddress(Key);
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
                            if (Value.Length < 6) error = $"Ваш {ValueName} слишком короткий.";
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