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
        public  new string ClassName
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
    }
}