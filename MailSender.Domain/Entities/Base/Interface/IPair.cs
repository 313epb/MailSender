namespace MailSender.Domain.Entities.Base.Interface
{
    public interface IPair : INamedEntity
    {
        string Key { get; set; }
        string KeyName { get; }
        string Value { get; set; }
        string ValueName { get; }
        int Id { get; set; }
    }
}