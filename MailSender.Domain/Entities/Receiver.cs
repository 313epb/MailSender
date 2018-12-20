using MailSender.Domain.Entities.Base;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс получателя
    /// </summary>
    public class Receiver:NamedEntity
    {
        /// <summary>
        /// Email получателя
        /// </summary>
        public string Email { get; set; }  
    }
}