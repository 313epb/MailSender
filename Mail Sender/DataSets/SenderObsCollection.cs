using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using MailSender.Domain.Entities;
using Mail_Sender.Model;
namespace Mail_Sender.DataSets
{
    public class SenderObsCollection:ObservableCollection<Sender>
    {
        private MailSenderContext _context;

        public SenderObsCollection()
        {
            _context = MailSenderContext.Instance;

            _context.Senders.Load(); //*
            foreach (Sender item in _context.Senders) //*
            {
                Add(item);
            }
        }

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public void AddSender(Sender item)
        {
            if ((_context.Senders.Where(x => x.Key == item.Key)).FirstOrDefault<Sender>() == null)
            {
                if (!string.IsNullOrEmpty(item.Key))
                {
                    Add(item);
                    _context.Senders.Add(item);
                    _context.Entry(item).State = EntityState.Added;
                }
                else
                {
                    MessageBox.Show($"{item.KeyName} отправителя не может быть пустым");

                }
                //throw ex
            }
            else
            {
                MessageBox.Show($"Отправитель с таким {item.KeyName} уже существует. {item.KeyName} должен быть уникальным");
            }
        }

        public void NotifySenderModified(Sender item)
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


        public void DeleteSender(Sender item)
        {
            Remove(item);
            _context.Entry(item).State = EntityState.Deleted;
            _context.Senders.Remove(item);
        }
    }
}
