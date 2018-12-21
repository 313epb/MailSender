namespace MailSender.Domain.Entities.Base.Interface
{
    public interface IPair:INamedEntity
    {
        string Key { get; set; }
        string Value { get; set; }
    }
}