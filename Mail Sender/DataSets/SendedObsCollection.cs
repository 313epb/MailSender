﻿using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MailSender.Domain.Entities;
using Mail_Sender.Model;

namespace Mail_Sender.DataSets
{
    class SendedObsCollection:ObservableCollection<Sended>
    {
        private MailSenderContext _context;
        private SendedReceiverObsCollection _sr;
        private SendedReceiverObsCollection SR
        {
            get => _sr;
            set => _sr = value;
        }

        public SendedObsCollection()
        {
            _context = MailSenderContext.Instance;
            SR = new SendedReceiverObsCollection();

            _context.Sendeds.Load(); //*
            foreach (Sended item in _context.Sendeds) //*
            {
                Add(item);
            }
        }

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public void AddSended(Sended item)
        {
            if (!string.IsNullOrEmpty(item.Name))
            {
                Add(item);
                _context.Sendeds.Add(item);
                _context.Entry(item).State = EntityState.Added;
            }

            foreach (SendedReceiver itemSendedReceiver in item.SendedReceivers)
            {
               SR.AddSendedReceiver(itemSendedReceiver);
            }
            SR.SaveContext();

        }

        public void NotifySendedModified(Sended item)
        {
            if ((_context.Sendeds.Where(x => x.Name == item.Name)).FirstOrDefault<Sended>() != null)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(item).State = EntityState.Added;
            }
        }


        public void DeleteSended(Sended item)
        {
            Remove(item);
            _context.Entry(item).State = EntityState.Deleted;
            _context.Sendeds.Remove(item);
        }
    }
}