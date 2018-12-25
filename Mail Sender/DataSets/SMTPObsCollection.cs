using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using MailSender.Domain.Entities;
using Mail_Sender.Model;

namespace Mail_Sender.DataSets
{
    class SMTPObsCollection:ObservableCollection<SMTP>
    {
        private MailSenderContext _context;

        public SMTPObsCollection()
        {
            _context = MailSenderContext.Instance;

            _context.SMTPs.Load(); //*
            foreach (SMTP item in _context.SMTPs) //*
            {
                Add(item);
            }
        }

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public void AddSMTP(SMTP item)
        {
            if ((_context.SMTPs.Where(x => x.Key == item.Key)).FirstOrDefault<SMTP>() == null)
            {
                if (!string.IsNullOrEmpty(item.Key))
                {
                    Add(item);
                    _context.SMTPs.Add(item);
                    _context.Entry(item).State = EntityState.Added;
                }
                else
                {
                    MessageBox.Show($"{item.KeyName} SMTP сервера не может быть пустым");

                }
                //throw ex
            }
            else
            {
                MessageBox.Show($"SMTP сервер с таким {item.KeyName} уже существует. {item.KeyName} должен быть уникальным");
            }
        }

        public void NotifySMTPModified(Sender item)
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


        public void DeleteSMTP(SMTP item)
        {
            Remove(item);
            _context.Entry(item).State = EntityState.Deleted;
            _context.SMTPs.Remove(item);
        }
    }
}
