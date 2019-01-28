using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using MailSender.Domain.Entities;
using Mail_Sender.Model;

namespace Mail_Sender.DataSets
{
    public class MailObsCollection:ObservableCollection<Mail>
    {
        private MailSenderContext _context;

        public MailObsCollection(MailSenderContext context)
        {
            _context = context;
            _context.Mails.Load();
            foreach (Mail contextMail in _context.Mails)
            {
                Add(contextMail);
            }
        }

        private void SaveContext()
        {
            _context.SaveChanges();
            _context.Database.BeginTransaction().Commit();
        }

        public Mail AddMail(Mail mail)
        {
            Add(mail);
            Mail newmail = _context.Mails.Add(mail);
            SaveContext();
            return newmail;
        }

        public void NotifyMailModified(Mail mail)
        {
            if ((_context.Mails.Where(x => x.Id == mail.Id)).FirstOrDefault<Mail>() != null)
            {
                _context.Entry(mail).State = EntityState.Modified;
            }
            else
            {
                Add(mail);
                _context.Entry(mail).State = EntityState.Added;
            }
            SaveContext();
        }

        public void DeleteMail(Mail mail)
        {
            Remove(mail);
            _context.Entry(mail).State = EntityState.Deleted;
            SaveContext();
        }
    }
}
