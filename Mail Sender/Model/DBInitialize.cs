using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Domain.Entities;
using MailSender.Domain.Entities.Base.Interface;

namespace Mail_Sender.Model
{
    public static class Names
    {
        public static string test = "Testststts";
    }

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

        private static List<IPair> _senders = new  List<IPair>
        {
            new Sender()
            {
                Id = 0,
                Key = "dsderugin@gmail.com",
                Value = "89224027506313epb"
            },
            new Sender()
            {
                Id = 0,
                Key = "rusoptsales@gmail.com",
                Value = "89224027506"
            },
            new Sender()
            {
                Id = 0,
                Key = "nnderygina@gmail.com",
                Value = "89224027506"
            }
        };

        public static List<IPair> Senders
        {
            get => _senders;
            set => _senders = value;
        }

        
    }

}
