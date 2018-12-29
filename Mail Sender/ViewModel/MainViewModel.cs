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
using Mail_Sender.DataSets;
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

        private MailObsCollection _mails = new MailObsCollection();

        public MailObsCollection Mails
        {
            get => _mails;
            set => _mails = value;
        }

        private readonly string _mailClassName = MailSender.Domain.Constants.ClassNamesConstants.MailClassName;
        public string MailsClassName => _mailClassName;

        #endregion

        #region Senders

        public IPair SelectedSender { get; set; }

        private IPairObsCollection _senders= new IPairObsCollection(ClassNamesConstants.SenderClassName);

        public IPairObsCollection Senders
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

        private IPairObsCollection _smtps= new IPairObsCollection(ClassNamesConstants.SMTPClassName);

        public IPairObsCollection SMTPs
        {
            get => _smtps;
            set => _smtps = value;
        }

        private readonly string _SMTPSClassName = MailSender.Domain.Constants.ClassNamesConstants.SMTPClassName;
        public string SMTPClassName => _SMTPSClassName;


        #endregion

        #region Receivers

        //private static ObservableCollection<IPair> _receivers = new ObservableCollection<IPair>
        //{
        //    new Receiver
        //    {
        //        Id = 0,
        //        Email = "dsderugin@gmail.com",
        //        ReceiverName = "Дмитрий",
        //        IsMailing = true
        //    },
        //    new Receiver
        //    {
        //        Id = 1,
        //        Email = "rusoptsales@gmail.com",
        //        ReceiverName = "Дмитрий"
        //    },
        //    new Receiver
        //    {
        //        Id = 2,
        //        Email = "uchihasaskekun@gmail.com",
        //        ReceiverName = "Дмитрий"
        //    },
        //    new Receiver
        //    {
        //        Id = 3,
        //        Email = "dsderyugin@gmail.com",
        //        ReceiverName = "Дмитрий"
        //    },
        //    new Receiver
        //    {
        //        Id = 4,
        //        Email = "deruginds@gmail.com",
        //        ReceiverName = "Дмитрий"
        //    }
        //};

        private IPairObsCollection _receivers= new IPairObsCollection(ClassNamesConstants.ReceiverClassName);

        public IPairObsCollection Receivers
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

        private CollectionViewSource _receiversViewSource;

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
        
        //private ObservableCollection<Sended> _history= new ObservableCollection<Sended>
        //{
        //    new Sended
        //    {
        //        Created = DateTime.Now,
        //        //Receievers = new ObservableCollection<Receiver>
        //        //{
        //        //    new Receiver
        //        //    {
        //        //        Email = "dsderugin@gmail.com",
        //        //        ReceiverName = "Alesha",
        //        //    },
        //        //    new Receiver
        //        //    {
        //        //        Email = "dsugin@gmail.com",
        //        //        ReceiverName = "Alesha1",
        //        //    },new Receiver
        //        //    {
        //        //        Email = "dsderu@gmail.com",
        //        //        ReceiverName = "Alesha2",
        //        //    },
        //        //},
        //        SMTP = new SMTP{SMTPName = "ya.ru", Port = "453"},
        //        Sender = new Sender{Email = "dsderugin@gmail.com",Password = "76147"},
        //        Mail = new Mail{Topic = "1e letter",Content = "asfqiouwyqw",Created = DateTime.Now,IsHTML = false},
        //    },

        //    new Sended
        //    {
        //        Created = DateTime.Now,
        //        //Receievers = new ObservableCollection<Receiver>()
        //        //{
        //        //    new Receiver
        //        //    {
        //        //        Email = "dsderugin@gmail.com",
        //        //        ReceiverName = "Alisa",
        //        //    },
        //        //    new Receiver
        //        //    {
        //        //        Email = "dsugin@gmail.com",
        //        //        ReceiverName = "Alha1",
        //        //    },new Receiver
        //        //    {
        //        //        Email = "dsderu@gmail.com",
        //        //        ReceiverName = "Aleks",
        //        //    },
        //        //},
        //        SMTP = new SMTP{SMTPName = "net.ru", Port = "453"},
        //        Sender = new Sender{Email = "dsderin@gmail.com",Password = "76147"},
        //        Mail = new Mail{Topic = "2e letter",Content = "aSubejctw",Created = DateTime.Now,IsHTML = true},
        //    },
        //};
        
            private SendedObsCollection _history= new SendedObsCollection();

        public SendedObsCollection History
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

        public RelayCommand<Mail> DeleteMailCommand { get; set; }

        public RelayCommand SendNowCommand { get; set; }

        public RelayCommand SendLaterCommand { get; set; }

        public RelayCommand<Sended> DeleteSendedCommand { get; set; }

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

                DeleteSendedCommand=new RelayCommand<Sended>(DeleteSended);

            #endregion

                _receiversViewSource = new CollectionViewSource() {Source = _receivers};
                _receiversViewSource.Filter += new FilterEventHandler(OnSendersCollectionViewSourceFilter);
            }

        

        #region Methods



        private void DeleteIPairItem(IPair item)
        {
            if (item.GetType() == typeof(Sender)){Senders.DeleteIPair(item);}
            if (item.GetType() == typeof(Receiver)){Receivers.DeleteIPair(item);}
            if (item.GetType() == typeof(SMTP)) {SMTPs.DeleteIPair(item);}
        }

        private void EditPairItem(IPair item)
        {
            if (item!=null)
            {
                AEPairItemWindow AEWindow = new AEPairItemWindow();
                AEWindow.Title = "Редактировать";
                AEWindow.Item = item;
                AEWindow.ShowDialog();
                MailSenderContext.Instance.SaveChanges();
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
                AEWindow.Item = new SMTP();
            }

            if (className == ClassNamesConstants.SenderClassName)
            {

                AEWindow.Item = new Sender();
            }

            if (className == ClassNamesConstants.ReceiverClassName)
            {
                AEWindow.Item = new Receiver();
            }

            AEWindow.ShowDialog();

            _item = AEWindow.Item;

            if (className == ClassNamesConstants.SMTPClassName)
            {
                SMTPs.AddIPair(_item);
            }

            if (className == ClassNamesConstants.SenderClassName)
            {
                Senders.AddIPair(_item);
            }

            if (className == ClassNamesConstants.ReceiverClassName)
            {
                Receivers.AddIPair(_item);
            }
        }

        private void SaveMail()
        {
            Mail temp;

            if ((temp = Mails.FirstOrDefault(m => m.Topic == SelectedMail.Topic)) != null)
            {
                temp.Topic = temp.Topic.ToString();
                temp.Created = DateTime.Now;
            }
            else
            {
                SelectedMail.Topic = SelectedMail.Topic.ToString();
                SelectedMail.Created = DateTime.Now;
                Mails.AddMail(SelectedMail);
            }

            SelectedMail = new Mail
            {
                Topic = SelectedMail.Topic.ToString(),
                Content = SelectedMail.Content,
                Created = new DateTime(),
                IsHTML = SelectedMail.IsHTML
            };

        }

        private void LoadMail()
        {
            LoadMailsWindow lmw= new LoadMailsWindow(Mails);
            lmw.ShowDialog();
            SelectedMail = lmw.Selected;
        }

        private void DeleteMail(Mail mail)
        {
            Mails.DeleteMail(mail);
        }

        private void SendNow()
        {
            Sended sn = new Sended
            {
                Mail = SelectedMail,
                Created = DateTime.Now,
                Sender = new Sender
                {
                    Key = SelectedSender.Key,
                    Value = SelectedSender.Value
                },
                SMTP = new SMTP
                {
                    Key = SelectdSMTP.Key,
                    Value = SelectdSMTP.Value
                },
                SendedReceivers = new ObservableCollection<SendedReceiver>()
            };

            foreach (Receiver receiver in Receivers)
            {
                if (receiver.IsMailing)
                {
                    SendedReceiver sr =new SendedReceiver
                    {
                        Receiver = receiver,
                        Sended = sn
                    };
                    sn.SendedReceivers.Add(sr);
                } 
            }

            //SendingMails.Send(sn);
            History.AddSended(sn);
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

        private void DeleteSended(Sended item)
        {
            History.DeleteSended(item);
        }

        #endregion




    }
}