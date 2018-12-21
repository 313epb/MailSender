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
        /// Имя получателя
        /// </summary>
        public string ReceiverName { get; set; }

        public static string ClassName { get => Constants.Constants.ClassNames.ReceiverClassName; }
    }
}