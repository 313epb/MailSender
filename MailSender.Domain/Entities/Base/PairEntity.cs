using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities.Base
{
    public class PairEntity:NamedEntity,IPair
    {
        public string Key { get; set; }
        public string KeyName { get; }
        public string Value { get; set; }
        public string ValueName { get; }
    }
}