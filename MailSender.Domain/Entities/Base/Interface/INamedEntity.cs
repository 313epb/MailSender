namespace MailSender.Domain.Entities.Base.Interface
{
    public interface INamedEntity:IBaseEntity
    {
        string Name { get; set; }
    }
}