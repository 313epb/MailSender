using MailSender.Domain.Entities.Base;

namespace MailSender.Domain.Entities
{

    /// <summary>
    /// Класс отправителя
    /// </summary>
    public class Sender:NamedEntity
    {
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

    }
}