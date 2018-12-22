using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс для SMTP серверов
    /// </summary>
    public class SMTP: NamedEntity,IPair
    {
        public static string ClassName
        {
            get => Constants.ClassNamesConstants.SMTPClassName;
        }

        public string SMTPName { get; set; }
        public string Port { get; set; }

        public  string Key { get=>SMTPName; set=>SMTPName=value; }
        public  string KeyName { get=>Constants.ClassNamesConstants.SMTPKeyName; }

        public  string Value { get=>Port; set=>Port=value; }
        public  string ValueName { get=>Constants.ClassNamesConstants.SMTPValueName; }
        string INamedEntity.ClassName => ClassName;
    }
}