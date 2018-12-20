using MailSender.Domain.Entities.Base;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс для SMTP серверов
    /// </summary>
    public class SMTP:NamedEntity
    {
        /// <summary>
        /// Имя порта
        /// </summary>
        public int PortName { get; set; }
    }
}