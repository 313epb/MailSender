using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public string Email { get; set; }
        public string ReceiverName { get; set; }

        public ICollection<SendedReceiver> SendedReceivers { get; set; }

        public Receiver()
        {
            SendedReceivers= new ObservableCollection<SendedReceiver>();
        }

        public bool IsMailing { get; set; }

        public override string ClassName { get => Constants.ClassNamesConstants.ReceiverClassName; }

        public override string Key { get=>Email; set=>Email=value; }
        public override string KeyName { get=>Constants.ClassNamesConstants.ReceiverKeyName;}
        
        public override string Value { get=>ReceiverName; set=>ReceiverName=value; }
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
    }
}