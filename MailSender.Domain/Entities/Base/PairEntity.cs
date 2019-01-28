using MailSender.Domain.Entities.Base.Interface;

namespace MailSender.Domain.Entities.Base
{
    public class PairEntity:NamedEntity,IPair
    {
        public virtual string Key { get; set; }
        public virtual string KeyName { get; }
        public virtual string Value { get; set; }
        public virtual string ValueName { get; }

        public virtual string Error { get; }

        public virtual string this[string columnName]
        {
            get => string.Empty;
        }


        
    }
}