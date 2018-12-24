using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities.Base
{
    public class NamedEntity:BaseEntity
    {
        public string ClassName { get; }
    }
}