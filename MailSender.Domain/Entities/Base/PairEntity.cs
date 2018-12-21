using System;
using System.Collections.Generic;
using System.Text;
using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities.Base
{
    public class PairEntity:NamedEntity,IPair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
