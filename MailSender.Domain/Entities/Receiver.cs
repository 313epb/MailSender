using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public string ClassName { get => Constants.ClassNamesConstants.ReceiverClassName; }

        public string Key { get=>Email; set=>Email=value; }
        public string KeyName { get=>Constants.ClassNamesConstants.ReceiverKeyName;}

        public string Value { get=>ReceiverName; set=>ReceiverName=value; }
        public string ValueName { get=>Constants.ClassNamesConstants.ReceiverValueName; }
    }
}