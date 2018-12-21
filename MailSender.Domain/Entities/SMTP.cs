using MailSender.Domain.Entities.Base;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс для SMTP серверов
    /// </summary>
    public class SMTP:PairEntity
    {
        public static string ClassName
        {
            get => Constants.ClassNamesConstants.SMTPClassName;
        }
    }
}