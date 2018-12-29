using System;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities.Base
{
    public class DateTimeEntity:BaseEntity,IDateTimeEntity
    {

        public virtual DateTime Created { get;set;}
    }
}