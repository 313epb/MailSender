using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities.Base
{
    public class BaseEntity:IBaseEntity
    {
        public int Id { get; set; }
    }
}