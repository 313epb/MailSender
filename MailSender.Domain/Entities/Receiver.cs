using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Text.RegularExpressions;
using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс получателя
    /// </summary>
    public class Receiver: PairEntity
    {

        public ICollection<SendedReceiver> SendedReceivers { get; set; }

        public Receiver()
        {
            SendedReceivers= new ObservableCollection<SendedReceiver>();
        }
        [NotMapped]
        public bool IsMailing { get; set; }

        public override string ClassName { get => Constants.ClassNamesConstants.ReceiverClassName; }

        public override string Key { get; set; }
        public override string KeyName { get=>Constants.ClassNamesConstants.ReceiverKeyName;}
        
        public override string Value { get; set; }
        public override string ValueName { get=>Constants.ClassNamesConstants.ReceiverValueName; }

        public static Receiver ConvertFromIPair(IPair item)
        {
            return new Receiver()
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
                            if (Value.Length < 2) error = $"Ваш {ValueName} слишком короткий.";
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