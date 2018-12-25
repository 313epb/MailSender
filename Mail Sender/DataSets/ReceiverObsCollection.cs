using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using MailSender.Domain.Entities;
using Mail_Sender.Model;

namespace Mail_Sender.DataSets
{
    public class ReceiverObsCollection : ObservableCollection<Receiver>
    {
        private MailSenderContext _context;

        public ReceiverObsCollection()
        {
            _context = MailSenderContext.Instance;

            _context.Receivers.Load();
            foreach (Receiver item in _context.Receivers)
            {
                Add(item);
            }
        }

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public void AddMail(Receiver item)
        {
            if ((_context.Receivers.Where(x => x.Email == item.Email)).FirstOrDefault<Receiver>() == null)
            {
                if (!string.IsNullOrEmpty(item.Email))
                {
                    Add(item);
                    _context.Receivers.Add(item);
                    _context.Entry(item).State = EntityState.Added;
                }
                else
                {
                    MessageBox.Show("Email получателя не может быть пустым");

                }
                //throw ex
            }
            else
            {
                MessageBox.Show("Получатель с таким Email уже существует. Email должен быть уникальным");
            }
        }

        public void NotifyMailModified(Receiver item)
        {
            if ((_context.Receivers.Where(x => x.Email == item.Email)).FirstOrDefault<Receiver>() != null)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(item).State = EntityState.Added;
            }
        }


        public void DeleteMail(Receiver item)
        {
            Remove(item);
            _context.Entry(item).State = EntityState.Deleted;
            _context.Receivers.Remove(item);
        }
    }
}
