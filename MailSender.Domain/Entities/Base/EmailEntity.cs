using System;
using System.Collections.Generic;
using System.Text;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities.Base
{
    public class EmailEntity:NamedEntity,IEmail
    {
        public string Email { get; set; }
    }
}
