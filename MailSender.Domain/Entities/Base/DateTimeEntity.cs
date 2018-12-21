using System;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities.Base
{
    public class DateTimeEntity:BaseEntity,IDateTimeEntity
    {
        public DateTime Created { get; set; }
    }
}