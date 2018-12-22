using GalaSoft.MvvmLight;
using MailSender.Domain.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using MailSender.Domain.Entities.Base.Interface;
using MailSender.Domain.Constants;
using Mail_Sender.View;
using System.Windows.Data;

namespace Mail_Sender.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        #region Mails

        private ObservableCollection<Mail> _mails = new ObservableCollection<Mail>
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

        public ObservableCollection<Mail> Mails
        {
            get => _mails;
            set => _mails = value;
        }

        private readonly string _mailClassName = MailSender.Domain.Constants.ClassNamesConstants.MailClassName;
        public string MailsClassName => _mailClassName;

        #endregion

        #region Senders

        private ObservableCollection<IPair> _senders = new ObservableCollection<IPair>
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

        public ObservableCollection<IPair> Senders
        {
            get => _senders;
            set
            {
                _senders = value;
                
            }
        }

        private readonly string _senderClassName = MailSender.Domain.Constants.ClassNamesConstants.SenderClassName;
        public string SenderClassName => _senderClassName;

        #endregion

        #region SMTPs

        private ObservableCollection<IPair> _smtps = new ObservableCollection<IPair>
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

        public ObservableCollection<IPair> SMTPs
        {
            get => _smtps;
            set => _smtps = value;
        }

        private readonly string _SMTPSClassName = MailSender.Domain.Constants.ClassNamesConstants.SMTPClassName;
        public string SMTPClassName => _SMTPSClassName;


        #endregion

        #region Receivers

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

        public ObservableCollection<IPair> Receivers
        {
            get => _receivers;
            set
            {
                _receivers = value;
            }
        }

        private string _filterName;

        public string FilterName
        {
            get => _filterName;
            set
            {
                if (!Set(ref _filterName, value)) return;
                ReceiversView.Refresh();
            }
        }

        private CollectionViewSource _receiversViewSource = new CollectionViewSource(){Source = _receivers};

        public ICollectionView ReceiversView => _receiversViewSource?.View;

        private void OnSendersCollectionViewSourceFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Receiver re) || string.IsNullOrWhiteSpace(_filterName)) return;
            if (!re.ReceiverName.Contains(_filterName))
                e.Accepted = false;
        }

        private readonly string _receiverClassName = MailSender.Domain.Constants.ClassNamesConstants.ReceiverClassName;
        public string ReceiverClassName => _receiverClassName;

        #endregion


        public RelayCommand<IPair> DeletePairCommand{get; set; }

        public RelayCommand<string> AddPairCommand{ get; set; }

        public RelayCommand<IPair> EditPairCommand { get; set; }

        public MainViewModel()
        {
            
            DeletePairCommand = new RelayCommand<IPair>(DeleteIPairItem);
            AddPairCommand= new RelayCommand<string>(AddPairItem);
            EditPairCommand= new RelayCommand<IPair>(EditPairItem);
            _receiversViewSource.Filter += new FilterEventHandler(OnSendersCollectionViewSourceFilter);
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