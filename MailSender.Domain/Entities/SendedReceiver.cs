using System.ComponentModel.DataAnnotations.Schema;
using MailSender.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MailSender.Domain.Entities
{
    public class SendedReceiver:BaseEntity
    {
        public int ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public Receiver Receiver { get; set; }

        public int SendedId { get; set; }
        [ForeignKey("ReceiverId")]
        public Sended  Sended { get; set; }
    }
}