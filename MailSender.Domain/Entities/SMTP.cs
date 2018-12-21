using MailSender.Domain.Entities.Base;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс для SMTP серверов
    /// </summary>
    public class SMTP:NamedEntity
    {
        public string ClassName
        {
            get => Constants.Constants.ClassNames.SMTPClassName;
        }

        /// <summary>
        /// Название SMTP сервера
        /// </summary>
        public string SMTPServer { get; set; }

        /// <summary>
        /// Имя порта
        /// </summary>
        public int PortName { get; set; }
    }
}