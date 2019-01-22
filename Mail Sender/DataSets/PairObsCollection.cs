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
            _set.Load();
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
            if (item.GetType()==typeof(Sender))
            {
                if (_context.Senders.FirstOrDefault(x => x.Key == item.Key) == null)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        Add(item);
                        _context.Entry(item).State = EntityState.Added;
                    }
                    else
                    {
                        MessageBox.Show($"{item.KeyName} не может быть пустым");

                    }
                }
                else
                {
                    MessageBox.Show($"{item.KeyName} должен быть уникальным");
                }
            }

            if (item.GetType() == typeof(Receiver))
            {
                if ((_context.Receivers.Where(x => x.Key == item.Key)).FirstOrDefault<Receiver>() == null)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        Add(item);
                        _context.Entry(item).State = EntityState.Added;
                    }
                    else
                    {
                        MessageBox.Show($"{item.KeyName} не может быть пустым");

                    }
                }
                else
                {
                    MessageBox.Show($"{item.KeyName} должен быть уникальным");
                }
            }

            if (item.GetType() == typeof(SMTP))
            {
                if ((_context.SMTPs.Where(x => x.Key == item.Key)).FirstOrDefault<SMTP>() == null)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        Add(item);
                        _context.Entry(item).State = EntityState.Added;
                    }
                    else
                    {
                        MessageBox.Show($"{item.KeyName} не может быть пустым");

                    }
                }
                else
                {
                    MessageBox.Show($"{item.KeyName} должен быть уникальным");
                }
            }
            
            SaveContext();
        }

        public void NotifyPairModified(IPair item)
        {
            IPair dbItem;

            if (item.GetType()==typeof(Sender))
            {
                if ((dbItem=_context.Senders.FirstOrDefault(x => x.Id == item.Id)) != null)
                {
                    dbItem.Key = item.Key;
                    dbItem.Value = item.Value;
                    _context.Entry(dbItem).State = EntityState.Modified;
                }
                else
                {
                    _context.Entry(item).State = EntityState.Added;
                }
            }
            if (item.GetType() == typeof(Receiver))
            {
                if ((dbItem= _context.Receivers.FirstOrDefault(x => x.Id == item.Id)) != null)
                {
                    dbItem.Key = item.Key;
                    dbItem.Value = item.Value;
                    _context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    _context.Entry(item).State = EntityState.Added;
                }
            }
            if (item.GetType() == typeof(SMTP))
            {
                if ((dbItem= _context.SMTPs.FirstOrDefault(x => x.Id == item.Id)) != null)
                {
                    dbItem.Key = item.Key;
                    dbItem.Value = item.Value;
                    _context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    _context.Entry(item).State = EntityState.Added;
                }
            }
            SaveContext();
        }

        public void DeleteIPair(IPair item)
        {

            if (item!=null)
            {
                Remove(item);

                _context.Entry(item).State = EntityState.Deleted;
            
                SaveContext();
                
            }
        }

        private int GetIndexOfItem(IPair item)
        {
            foreach (IPair pair in this)
            {
                if (pair.Id == item.Id) return IndexOf(pair);
            }

            return -1;
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
