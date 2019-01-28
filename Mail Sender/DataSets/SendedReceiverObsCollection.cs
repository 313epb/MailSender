using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using MailSender.Domain.Entities;
using Mail_Sender.Model;

namespace Mail_Sender.DataSets
{
    public class SendedReceiverObsCollection : ObservableCollection<SendedReceiver>
    {
        private MailSenderContext _context;

        public SendedReceiverObsCollection(MailSenderContext context)
        {
            _context = context;

            _context.SendedReceivers.Load(); //*
            foreach (SendedReceiver item in _context.SendedReceivers) //*
            {
                Add(item);
            }
        }

        public void SaveContext()
        {
            _context.SaveChanges();
            _context.Database.BeginTransaction().Commit();
        }

        public void AddSendedReceiver(SendedReceiver item) //*
        {
            Add(item);
            _context.Entry(item).State = EntityState.Added;
        }

        public void NotifySendedReceiverModified(SendedReceiver item) //*
        {
            if ((_context.SendedReceivers.Where(x => x.SendedId == item.SendedId)
                    .Where(x => x.ReceiverId == item.ReceiverId)).FirstOrDefault<SendedReceiver>() != null)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(item).State = EntityState.Added;
            }
        }


        public void DeleteSendedReceiver(SendedReceiver item)
        {
            Remove(item);
            _context.Entry(item).State = EntityState.Deleted;
        }
    }
}