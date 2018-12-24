using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MailSender.Domain.Entities
{
    public class SendedReceiver
    {
        public int? ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public Receiver Receiver { get; set; }

        public int? SendedId { get; set; }

        [ForeignKey("SendedId")]
        public Sended Sended { get; set; }
    }
}