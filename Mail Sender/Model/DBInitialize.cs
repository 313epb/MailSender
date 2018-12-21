using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Domain.Entities;

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
                Name = "Первое письмо"
            },
            new Mail
            {
                Id = 1,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = true,
                Name = "Второе письмо"
            },
            new Mail
            {
                Id = 2,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = true,
                Name = "Третье письмо"
            },
            new Mail
            {
                Id = 3,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = false,
                Name = "Четвертое письмо"
            },
            new Mail
            {
                Id = 4,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = true,
                Name = "Пятое письмо"
            }
        };

        public static List<Mail> Mails
        {
            get => _mails;
            set => _mails = value;
        }

        private static List<Sender> _senders = new  List<Sender>
        {
            new Sender()
            {
                Id = 0,
                Name = "dsderugin@gmail.com",
                Password = "89224027506313epb"
            },
            new Sender()
            {
                Id = 0,
                Name = "rusoptsales@gmail.com",
                Password = "89224027506"
            },
            new Sender()
            {
                Id = 0,
                Name = "nnderygina@gmail.com",
                Password = "89224027506"
            }
        };

        public static List<Sender> Senders
        {
            get => _senders;
            set => _senders = value;
        }

        
    }

}
