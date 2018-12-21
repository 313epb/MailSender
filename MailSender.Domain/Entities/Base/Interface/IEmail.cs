namespace MailSender.Domain.Entities.Base.Interface
{
    public interface IEmail:INamedEntity
    {
        string Email { get; set; }
    }
}