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

        public IPairObsCollection(string className)
        {
            _context = MailSenderContext.Instance;

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

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public void AddIPair(IPair item)
        {
            if (item.GetType()==typeof(Sender))
            {
                if ((_context.Senders.Where(x => x.Key == item.Key)).FirstOrDefault<Sender>() == null)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        Add(Sender.ConvertFromIPair(item));
                        _context.Senders.Add(Sender.ConvertFromIPair(item));
                        _context.Entry(item).State = EntityState.Added;
                        _context.SaveChanges();
                        _context.Database.BeginTransaction().Commit();
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

            if (item.ClassName == ClassNamesConstants.ReceiverClassName)
            {
                if ((_context.Receivers.Where(x => x.Key == item.Key)).FirstOrDefault<Receiver>() == null)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        Add(item);
                        _context.Receivers.Add(Receiver.ConvertFromIPair(item));
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

            if (item.ClassName == ClassNamesConstants.SMTPClassName)
            {
                if ((_context.SMTPs.Where(x => x.Key == item.Key)).FirstOrDefault<SMTP>() == null)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        Add(item);
                        _context.SMTPs.Add(SMTP.ConvertFromIPair(item));
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

            _context.SaveChanges();
        }

        public void NotifyIPairModified(IPair item)
        {
            if (item.ClassName == ClassNamesConstants.SenderClassName)
            {
                if ((_context.Senders.Where(x => x.Key == item.Key)).FirstOrDefault<Sender>() != null)
                {
                    _context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    _context.Entry(item).State = EntityState.Added;
                }
            }

            if (item.ClassName == ClassNamesConstants.ReceiverClassName)
            {
                if ((_context.Receivers.Where(x => x.Key == item.Key)).FirstOrDefault<Receiver>() != null)
                {
                    _context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    _context.Entry(item).State = EntityState.Added;
                }
            }

            if (item.ClassName == ClassNamesConstants.SMTPClassName)
            {
                if ((_context.SMTPs.Where(x => x.Key == item.Key)).FirstOrDefault<SMTP>() != null)
                {
                    _context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    _context.Entry(item).State = EntityState.Added;
                }
            }

        }

        public void DeleteIPair(IPair item)
        {
            Remove(item);
            _context.Entry(item).State = EntityState.Deleted;

            if (item.ClassName == ClassNamesConstants.SenderClassName) _context.Senders.Remove(Sender.ConvertFromIPair(item));

            if (item.ClassName == ClassNamesConstants.ReceiverClassName) _context.Receivers.Remove(Receiver.ConvertFromIPair(item));

            if (item.ClassName == ClassNamesConstants.SMTPClassName) _context.SMTPs.Remove(SMTP.ConvertFromIPair(item));
        }
    }
}
