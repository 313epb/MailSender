using GalaSoft.MvvmLight;
using MailSender.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using MailSender.Domain.Entities.Base.Interface;
using MailSender.Domain.Constants;
using Mail_Sender.View;

namespace Mail_Sender.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private static List<Mail> _mails = new List<Mail>
        {
            new Mail
            {
                Id = 0,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = false,
                Topic = "������ ������"
            },
            new Mail
            {
                Id = 1,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = true,
                Topic = "������ ������"
            },
            new Mail
            {
                Id = 2,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = true,
                Topic = "������ ������"
            },
            new Mail
            {
                Id = 3,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = false,
                Topic = "��������� ������"
            },
            new Mail
            {
                Id = 4,
                Content = "Hello World!",
                Created = DateTime.Now,
                IsHTML = true,
                Topic = "����� ������"
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

        private static ObservableCollection<IPair> _receivers = new ObservableCollection<IPair>
        {
            new Receiver
            {
                Id = 0,
                Email = "dsderugin@gmail.com",
                ReceiverName = "�������"
            },
            new Receiver
            {
                Id = 1,
                Email = "rusoptsales@gmail.com",
                ReceiverName = "�������"
            },
            new Receiver
            {
                Id = 2,
                Email = "uchihasaskekun@gmail.com",
                ReceiverName = "�������"
            },
            new Receiver
            {
                Id = 3,
                Email = "dsderyugin@gmail.com",
                ReceiverName = "�������"
            },
            new Receiver
            {
                Id = 4,
                Email = "deruginds@gmail.com",
                ReceiverName = "�������"
            }
        };

        public static ObservableCollection<IPair> Receivers
        {
            get => _receivers;
            set => _receivers = value;
        }

        public RelayCommand<IPair> DeletePairCommand{get; set; }

        public RelayCommand<IPair> AddPairCommand{ get; }
        

        public static Receiver rec=new Receiver();

        public MainViewModel()
        {
            DeletePairCommand= new RelayCommand<IPair>(DeleteIPairItem);
            AddPairCommand= new RelayCommand<IPair>(AddPairItem);
        }

        private void DeleteIPairItem(IPair item)
        {
            if (item.ClassName == ClassNamesConstants.SMTPClassName){SMTPs.Remove(item);}
            if (item.ClassName == ClassNamesConstants.SenderClassName){Senders.Remove(item);}
            if (item.ClassName == ClassNamesConstants.ReceiverClassName){Receivers.Remove(item);}
        }

        private void AddPairItem(IPair item)
        {
            AEPairItemWindow AEWindow = new AEPairItemWindow();
            AEWindow.Title = "��������";
            AEWindow.Show();
        }
    }
}