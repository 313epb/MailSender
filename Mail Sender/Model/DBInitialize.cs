using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Domain.Entities;
using MailSender.Domain.Entities.Base.Interface;

namespace Mail_Sender.Model
{

    class DBInitialize
    {

        private static List<Mail> _mails = new List<Mail>
        {
            new Mail
            {
                Id = 0,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = false,
                Topic = "Первое письмо"
            },
            new Mail
            {
                Id = 1,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = true,
                Topic = "Второе письмо"
            },
            new Mail
            {
                Id = 2,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = true,
                Topic = "Третье письмо"
            },
            new Mail
            {
                Id = 3,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = false,
                Topic = "Четвертое письмо"
            },
            new Mail
            {
                Id = 4,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = true,
                Topic = "Пятое письмо"
            }
        };

        public static List<Mail> Mails
        {
            get => _mails;
            set => _mails = value;
        }

        private static ObservableCollection<IPair> _senders = new ObservableCollection<IPair>
        {
            new Sender()
            {
                Id = 0,
                Key = "dsderugin@gmail.com",
                Value = "892asdasdas"
            },
            new Sender()
            {
                Id = 0,
                Key = "rusoptsales@gmail.com",
                Value = "8dasdasdf7506"
            },
            new Sender()
            {
                Id = 0,
                Key = "nnderygina@gmail.com",
                Value = "89sdfssgsg6"
            }
        };

        public static ObservableCollection<IPair> Senders
        {
            get => _senders;
            set => _senders = value;
        }

        private static ObservableCollection<IPair> _smtps = new ObservableCollection<IPair>
        {
            new SMTP
            {
                Id = 0,
                Key = "smtp.gmail.com",
                Value = "587"
            },
            new SMTP
            {
                Id = 1,
                Key = "smtp.yandex.ru",
                Value = "465"
            },
            new SMTP
            {
                Id = 2,
                Key = "smtp.mail.ru",
                Value = "465"
            }
        };

        public static ObservableCollection<IPair> SMTPs
        {
            get => _smtps;
            set => _smtps = value;
        }

        private static ObservableCollection<Receiver> _receivers= new ObservableCollection<Receiver>
        {
            new Receiver
            {
                Id = 0,
                Email = "dsderugin@gmail.com",
                ReceiverName = "Дмитрий"
            },
            new Receiver
            {
                Id = 1,
                Email = "rusoptsales@gmail.com",
                ReceiverName = "Дмитрий"
            },
            new Receiver
            {
                Id = 2,
                Email = "uchihasaskekun@gmail.com",
                ReceiverName = "Дмитрий"
            },
            new Receiver
            {
                Id = 3,
                Email = "dsderyugin@gmail.com",
                ReceiverName = "Дмитрий"
            },
            new Receiver
            {
                Id = 4,
                Email = "deruginds@gmail.com",
                ReceiverName = "Дмитрий"
            },
        };

        public static ObservableCollection<Receiver> Receivers
        {
            get => _receivers;
            set => _receivers = value;
        }
        
    }

}
