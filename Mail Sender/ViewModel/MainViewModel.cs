using GalaSoft.MvvmLight;
using MailSender.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using MailSender.Domain.Entities.Base.Interface;
using MailSender.Domain.Constants;
using Mail_Sender.View;
using System.Windows.Data;
using Mail_Sender.Model;

namespace Mail_Sender.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        #region Mails

        private Mail _selectedMail= new Mail();

        public Mail SelectedMail
        {
            get => _selectedMail;
            set
            {
                _selectedMail = value;
                RaisePropertyChanged(nameof(SelectedMail));
            }
        }

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

        public IPair SelectedSender { get; set; }

        private ObservableCollection<IPair> _senders = new ObservableCollection<IPair>
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

        public IPair SelectdSMTP { get; set; }

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
                ReceiverName = "Дмитрий",
                IsMailing = true
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

        #region History
        
        private ObservableCollection<Sended> _history= new ObservableCollection<Sended>
        {
            new Sended
            {
                Created = DateTime.Now,
                //Receievers = new ObservableCollection<Receiver>
                //{
                //    new Receiver
                //    {
                //        Email = "dsderugin@gmail.com",
                //        ReceiverName = "Alesha",
                //    },
                //    new Receiver
                //    {
                //        Email = "dsugin@gmail.com",
                //        ReceiverName = "Alesha1",
                //    },new Receiver
                //    {
                //        Email = "dsderu@gmail.com",
                //        ReceiverName = "Alesha2",
                //    },
                //},
                SMTP = new SMTP{SMTPName = "ya.ru", Port = "453"},
                Sender = new Sender{Email = "dsderugin@gmail.com",Password = "76147"},
                Mail = new Mail{Topic = "1e letter",Content = "asfqiouwyqw",Created = DateTime.Now,IsHTML = false},
            },

            new Sended
            {
                Created = DateTime.Now,
                //Receievers = new ObservableCollection<Receiver>()
                //{
                //    new Receiver
                //    {
                //        Email = "dsderugin@gmail.com",
                //        ReceiverName = "Alisa",
                //    },
                //    new Receiver
                //    {
                //        Email = "dsugin@gmail.com",
                //        ReceiverName = "Alha1",
                //    },new Receiver
                //    {
                //        Email = "dsderu@gmail.com",
                //        ReceiverName = "Aleks",
                //    },
                //},
                SMTP = new SMTP{SMTPName = "net.ru", Port = "453"},
                Sender = new Sender{Email = "dsderin@gmail.com",Password = "76147"},
                Mail = new Mail{Topic = "2e letter",Content = "aSubejctw",Created = DateTime.Now,IsHTML = true},
            },
        };
        
        public ObservableCollection<Sended> History
        {
            get => _history;
            set => _history = value;
        }

        private DateTime _selectedTime = DateTime.Now;
        public DateTime SelectedTime
        {
            get => _selectedTime; set => _selectedTime = value;
        }

        #endregion

        #region Commands

        public RelayCommand<IPair> DeletePairCommand{get; set; }

        public RelayCommand<string> AddPairCommand{ get; set; }

        public RelayCommand<IPair> EditPairCommand { get; set; }

        public RelayCommand LoadMailCommand { get; set; }

        public RelayCommand SaveMailCommand { get; set; }

        public RelayCommand SendNowCommand { get; set; }

        public RelayCommand SendLaterCommand { get; set; }

        public RelayCommand<Mail> DeleteMailCommand { get; set; }

        #endregion


        public MainViewModel()
        {
            #region CommandsIni

            DeletePairCommand = new RelayCommand<IPair>(DeleteIPairItem);
            AddPairCommand= new RelayCommand<string>(AddPairItem);
            EditPairCommand= new RelayCommand<IPair>(EditPairItem);

            LoadMailCommand= new RelayCommand(LoadMail);
            SaveMailCommand= new RelayCommand(SaveMail);
            DeleteMailCommand= new RelayCommand<Mail>(DeleteMail);

            SendNowCommand = new RelayCommand(SendNow);
            SendLaterCommand= new RelayCommand(SendLater);

            #endregion
            
            _receiversViewSource.Filter += new FilterEventHandler(OnSendersCollectionViewSourceFilter);
        }
        
        private void SaveMail()
        {
            Mail temp;
            if ((temp = Mails.FirstOrDefault(m => m.Topic == SelectedMail.Topic)) != null) temp = SelectedMail;
            else
            {
                SelectedMail.Id = Mails.Max(m => m.Id)+1;
                Mails.Add(SelectedMail);
            }
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
            MailSenderContext msContext= new MailSenderContext();
            //msContext.Mails.Add(new Mail()
            //{
            //    Content = "asdasd",
            //    Created = DateTime.Now,
            //    IsHTML = true,
            //    Topic = "asdasd"
            //});
            //msContext.SaveChanges();
            
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

        private void LoadMail()
        {
            LoadMailsWindow lmw= new LoadMailsWindow(Mails);
            lmw.ShowDialog();
            SelectedMail = lmw.Selected;
        }

        private void SendNow()
        {
            Sended sn = new Sended
            {
                Mail = SelectedMail,
                Created = DateTime.Now,
                Id = History.Max(s => s.Id) + 1,
                Sender = Sender.ConvertFromIPair(SelectedSender),
                SMTP = SMTP.ConvertFromIPair(SelectdSMTP),
                //Receievers = new ObservableCollection<Receiver>()
            };

            //foreach (Receiver receiver in Receivers)
            //{
            //    if (receiver.IsMailing) sn.Receievers.Add(receiver); 
            //}

            SendingMails.Send(sn);
            History.Add(sn);
        }

        private void SendLater()
        {
            Sended sn = new Sended
            {
                Mail = SelectedMail,
                Created = SelectedTime,
                Id = History.Max(s => s.Id) + 1,
                Sender = Sender.ConvertFromIPair(SelectedSender),
                SMTP = SMTP.ConvertFromIPair(SelectdSMTP),
                //Receievers = new ObservableCollection<Receiver>()
            };

            //foreach (Receiver receiver in Receivers)
            //{
            //    if (receiver.IsMailing) sn.Receievers.Add(receiver);
            //}
        }

        private void DeleteMail(Mail mail)
        {
            Mails.Remove(mail);
        }

    }
}