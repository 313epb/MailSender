using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс получателя
    /// </summary>
    public class Receiver:EmailEntity
    {
        /// <summary>
        /// Email получателя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Имя получателя
        /// </summary>
        public string ReceiverName { get; set; }

        public string ClassName { get => Constants.Constants.ClassNames.ReceiverClassName; }
    }
}