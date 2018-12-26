using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс для SMTP серверов
    /// </summary>
    public class SMTP: PairEntity
    {
        public string ClassName
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
    }
}