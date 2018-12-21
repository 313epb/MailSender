using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{
    /// <summary>
    /// Класс получателя
    /// </summary>
    public class Receiver:PairEntity
    {
        public static string ClassName { get => Constants.ClassNamesConstants.ReceiverClassName; }
    }
}