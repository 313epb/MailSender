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
    public class IPairObsCollection : ObservableCollection<IPair>
    {

        private MailSenderContext _context;

        public IPairObsCollection(string className, MailSenderContext context)
        {
            _context = context;

            if (className == ClassNamesConstants.SenderClassName)
            {
                _context.Senders.Load();
                foreach (Sender item in _context.Senders)
                {
                    Add(item);
                }
            }

            if (className == ClassNamesConstants.ReceiverClassName)
            {
                _context.Receivers.Load();
                foreach (Receiver item in _context.Receivers)
                {
                    Add(item);
                }
            }

            if (className == ClassNamesConstants.SMTPClassName)
            {
                _context.SMTPs.Load();
                foreach (SMTP item in _context.SMTPs)
                {
                    Add(item);
                }
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
                if ((_context.Senders.Where(x => x.Key == item.Key)).FirstOrDefault<Sender>() == null)
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
            SetItem(GetIndexOfItem(item),item);

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
    }

   
}
