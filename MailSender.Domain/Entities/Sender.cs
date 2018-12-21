using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities
{

    /// <summary>
    /// Класс отправителя
    /// </summary>
    public class Sender:PairEntity
    {
        /// <summary>
        /// Название класса
        /// </summary>
        public static string ClassName
        {
            get => Constants.ClassNamesConstants.SenderClassName;
        }
    }
}