using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using MailSender.Domain.Entities;
using Mail_Sender.Model;
namespace Mail_Sender.DataSets
{
    public class SendedObsCollection:ObservableCollection<Sended>
    {
        private MailSenderContext _context;

        public SendedObsCollection()
        {
            _context = MailSenderContext.Instance;

            _context.Sendeds.Load(); //*
            foreach (Sended item in _context.Sendeds) //*
            {
                Add(item);
            }
        }

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public void AddSended(Sended item)
        {
            Add(item);
            _context.Entry(item).State = EntityState.Added;
            
            SendedReceiverObsCollection sro= new SendedReceiverObsCollection();
            int sendedid = _context.Sendeds.FirstOrDefault(sr => sr.Name == item.Name).Id;

            foreach (SendedReceiver sritem in item.SendedReceivers)
            {
                sritem.ReceiverId = _context.Receivers.FirstOrDefault(rc => rc.Key == sritem.Receiver.Key).Id;
                sritem.SendedId = sendedid;
                sro.Add(sritem);
            }

            sro.SaveContext();
        }

        public void NotifySendedModified(Sended item)
        {
            if ((_context.Sendeds.Where(x => x.Created == item.Created)).FirstOrDefault<Sended>() != null)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(item).State = EntityState.Added;
            }
        }


        public void DeleteSender(Sended item)
        {
            Remove(item);

            SendedReceiverObsCollection sro = new SendedReceiverObsCollection();
            foreach (SendedReceiver sritem in item.SendedReceivers)
            {
                sro.Remove(sritem);
            }

            _context.Entry(item).State = EntityState.Deleted;
            _context.Sendeds.Remove(item);
        }
    }
}
