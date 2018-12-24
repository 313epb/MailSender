namespace MailSender.Domain.Entities
{
    public class SendedReceiver
    {
        public int ReceiverId { get; set; }
        public Receiver Receiver { get; set; }

        public int SendedId { get; set; }
        public Sended Sended { get; set; }
    }
}