using GalaSoft.MvvmLight;
using MailSender.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MailSender.Domain.Entities.Base.Interface;
using MailSender.Domain.Constants;
using Mail_Sender.View;
using MailSender.Domain.Entities.Base;

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

        private static ObservableCollection<IPair> _receivers = new ObservableCollection<IPair>
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
            }
        };

        public static ObservableCollection<IPair> Receivers
        {
            get => _receivers;
            set => _receivers = value;
        }

        public RelayCommand<IPair> DeletePairCommand{get; set; }

        public RelayCommand<string> AddPairCommand{ get; set; }

        public RelayCommand<IPair> EditPairCommand { get; set; }

        public static Receiver rec=new Receiver();

        public MainViewModel()
        {
            DeletePairCommand= new RelayCommand<IPair>(DeleteIPairItem);
            AddPairCommand= new RelayCommand<string>(AddPairItem);
            EditPairCommand= new RelayCommand<IPair>(EditPairItem);
        }

        private void DeleteIPairItem(IPair item)
        {
            if (item.ClassName == ClassNamesConstants.SMTPClassName){SMTPs.Remove(item);}
            if (item.ClassName == ClassNamesConstants.SenderClassName){Senders.Remove(item);}
            if (item.ClassName == ClassNamesConstants.ReceiverClassName){Receivers.Remove(item);}
        }

        private void EditPairItem(IPair item)
        {
            if (item!=null)
            {
                AEPairItemWindow AEWindow = new AEPairItemWindow();
                AEWindow.Title = "Редактировать";
                AEWindow.Item = item;
                AEWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите значение для редактирования");
            }

        }

        private void AddPairItem(string className)
        {
            IPair _item;
            AEPairItemWindow AEWindow = new AEPairItemWindow();
            AEWindow.Title = "Добавить";
            if (className == ClassNamesConstants.SMTPClassName)
            {
                _item = new SMTP();
                _item.Id = SMTPs.Max(s => s.Id) + 1;
                AEWindow.Item = _item;
            }

            if (className == ClassNamesConstants.SenderClassName)
            {
                _item = new Sender();
                _item.Id = Senders.Max(s => s.Id) + 1;
                AEWindow.Item = _item;
            }

            if (className == ClassNamesConstants.ReceiverClassName)
            {
                _item = new Receiver();
                _item.Id = Receivers.Max(s => s.Id) + 1;
                AEWindow.Item = _item;
            }
            AEWindow.ShowDialog();
                if (className == ClassNamesConstants.SMTPClassName)
                {
                    SMTPs.Add(AEWindow.Item);
                }

                if (className == ClassNamesConstants.SenderClassName)
                {
                    Senders.Add(AEWindow.Item);
                }

                if (className == ClassNamesConstants.ReceiverClassName)
                {
                    Receivers.Add(AEWindow.Item);
                }
        }
    }
}