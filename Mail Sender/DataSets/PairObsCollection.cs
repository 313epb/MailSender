using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using MailSender.Domain.Entities;
using MailSender.Domain.Entities.Base.Interface;
using MailSender.Domain.Constants;
using Mail_Sender.Model;

namespace Mail_Sender.DataSets
{
    public class PairObsCollection : ObservableCollection<IPair>
    {

        private MailSenderContext _context;

        private DbSet _set;

        public PairObsCollection(IPair item, MailSenderContext context)
        {
            _context = context;
            _set = ForDBSet(item);
            _set?.Load();
            foreach (IPair pair in _set)
            {
                Add(pair);
            }
        }

        private void SaveContext()
        {
            _context.SaveChanges();
            _context.Database.BeginTransaction().Commit();
        }

        public void AddIPair(IPair item)
        {
            Add(item);
            _context.Entry(item).State = EntityState.Added;
            SaveContext();
        }

        public void NotifyPairModified(IPair item)
        {
            _context.Entry(item).State = EntityState.Modified;
            SaveContext();
        }

        public void DeleteIPair(IPair item)
        {
            Remove(item);
            _context.Entry(item).State = EntityState.Deleted;
            SaveContext();
        }
        

        private DbSet ForDBSet(IPair item)
        {
            if (item.GetType() == typeof(Sender)) return _context.Senders;
            if (item.GetType() == typeof(Receiver)) return _context.Receivers;
            if (item.GetType() == typeof(SMTP)) return _context.SMTPs;
            return null;
        }
    }

   
}
