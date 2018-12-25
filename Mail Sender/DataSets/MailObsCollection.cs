﻿using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using MailSender.Domain.Entities;
using Mail_Sender.Model;

namespace Mail_Sender.DataSets
{
    class MailObsCollection:ObservableCollection<Mail>
    {
        private MailSenderContext _context;

        public MailObsCollection()
        {
            _context = MailSenderContext.Instance;
            _context.Mails.Load();
            foreach (Mail contextMail in _context.Mails)
            {
                Add(contextMail);
            }
        }

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public void AddMail(Mail mail)
        {
            if ((_context.Mails.Where(x => x.Topic == mail.Topic)).FirstOrDefault<Mail>() == null)
            {
                if (!string.IsNullOrEmpty(mail.Topic))
                {
                    Add(mail);
                    _context.Mails.Add(mail);
                    _context.Entry(mail).State = EntityState.Added;
                }
                else
                {
                    MessageBox.Show("Имя письма не может быть пустым");

                }
                //throw ex
            }
            else
            {
                MessageBox.Show("Письмо с таким именем уже существует. Имя должно быть уникальным");
            }

        }

        public void NotifyMailModified(Mail mail)
        {
            if ((_context.Mails.Where(x => x.Topic == mail.Topic)).FirstOrDefault<Mail>() != null)
            {
                _context.Entry(mail).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(mail).State = EntityState.Added;
            }
        }


        public void DeleteMail(Mail mail)
        {
            Remove(mail);
            _context.Entry(mail).State = EntityState.Deleted;
            _context.Mails.Remove(mail);
        }
    }
}