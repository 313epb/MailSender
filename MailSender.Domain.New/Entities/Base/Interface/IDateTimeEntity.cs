using System;

namespace MailSender.Domain.Entities.Base.Interface
{
    public interface IDateTimeEntity:IBaseEntity
    {
        DateTime Created { get; set; }
    }
}