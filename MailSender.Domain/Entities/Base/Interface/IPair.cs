using System.ComponentModel;

namespace MailSender.Domain.Entities.Base.Interface
{
    public interface IPair : INamedEntity, IDataErrorInfo
    {
        string Key { get; set; }
        string KeyName { get; }
        string Value { get; set; }
        string ValueName { get; }
    }
}