using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailSender.Domain.Entities.Base.Interface
{
    public interface IBaseEntity
    {
        //Базовая сущность
        
        int Id { get; set; }
    }
}