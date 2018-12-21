using MailSender.Domain.Entities.Base;

namespace MailSender.Domain.Entities
{

    /// <summary>
    /// Класс отправителя
    /// </summary>
    public class Sender:NamedEntity
    {
        /// <summary>
        /// Название класса
        /// </summary>
        public readonly string ClassTitle = Constants.Constants.SenderClassName;
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

    }
}