using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{

    /// <summary>
    /// Класс отправителя
    /// </summary>
    public class Sender:EmailEntity
    {
        /// <summary>
        /// Название класса
        /// </summary>
        public string ClassName
        {
            get => Constants.Constants.ClassNames.SenderClassName;
        }

        /// <summary>
        /// Email отправителя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

    }
}