using GalaSoft.MvvmLight;
using MailSender.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using MailSender.Domain.Entities.Base.Interface;
using Mail_Sender.View;
using System.Windows.Data;
using Mail_Sender.DataSets;
using Mail_Sender.Model;
using System.Threading;
using MailSender.Domain.Constants;

namespace Mail_Sender.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        #region Senders

        public IPair SelectedSender { get; set; }

        private PairObsCollection _senders;
        public PairObsCollection Senders
        {
            get => _senders;
            set
            {
                _senders = value;
                
            }
        }

        private readonly string _senderClassName = ClassNamesConstants.SenderClassName;
        public string SenderClassName => _senderClassName;

        private IPair _objSender = new Sender();

        public IPair ObjSender { get => _objSender; set => _objSender = value; }

        #endregion

        #region Receivers

        private PairObsCollection _receivers;

        public PairObsCollection Receivers
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
            if (!re.Value.Contains(_filterName))
                e.Accepted = false;
        }

        private readonly string _receiverClassName = ClassNamesConstants.ReceiverClassName;
        public string ReceiverClassName => _receiverClassName;

        private IPair _objReceiver = new Receiver();

        public IPair OBjReceiver { get => _objReceiver; set => _objReceiver = value; }

        #endregion

        #region SMTPs

        public IPair SelectdSMTP { get; set; }

        private PairObsCollection _smtps;

        public PairObsCollection SMTPs
        {
            get => _smtps;
            set => _smtps = value;
        }

        private readonly string _SMTPSClassName = ClassNamesConstants.SMTPClassName;
        public string SMTPClassName => _SMTPSClassName;

        private IPair _objSmtp = new SMTP();

        public IPair OBjSmtp { get => _objSmtp; set => _objSmtp = value; }

        #endregion

        #region Mails

        private Mail _selectedMail = new Mail
        {
            Id = -1,
            Topic = string.Empty,
            Content = string.Empty
        };

        public Mail SelectedMail
        {
            get => _selectedMail;
            set
            {
                _selectedMail = value;
                RaisePropertyChanged(nameof(SelectedMail));
            }
        }

        private MailObsCollection _mails;

        public MailObsCollection Mails
        {
            get => _mails;
            set => _mails = value;
        }

        private readonly string _mailClassName = ClassNamesConstants.MailClassName;
        public string MailsClassName => _mailClassName;



        #endregion

        #region History

        private SendedObsCollection _history;

        public SendedObsCollection History
        {
            get => _history;
            set => _history = value;
        }

        private DateTime _selectedTime = DateTime.Now;
        public string SelectedTime
        {
            get => _selectedTime.ToString(OtherConstants.DateTimeFormat); set => _selectedTime = DateTime.Parse(value);
        }

        #endregion

        #region Commands

        public RelayCommand<IPair> DeletePairCommand{get; set; }

        public RelayCommand<IPair> AddPairCommand{ get; set; }

        public RelayCommand<IPair> EditPairCommand { get; set; }

        public RelayCommand LoadMailCommand { get; set; }

        public RelayCommand<Mail> SaveMailCommand { get; set; }

        public RelayCommand<Mail> DeleteMailCommand { get; set; }

        public RelayCommand<string> SendCommand { get; set; }

        public RelayCommand<Sended> DeleteSendedCommand { get; set; }

        public RelayCommand NewMailCommand { get; set; }

        public MailSenderContext _context { get; }

        #endregion

        public MainViewModel(MailSenderContext context)
        {
            _context = context;

            #region Ini

            _senders = new PairObsCollection(new Sender(), _context);
            _receivers = new PairObsCollection(new Receiver(), _context);
            _smtps = new PairObsCollection(new SMTP(), _context);

            _mails = new MailObsCollection(_context);

            _history = new SendedObsCollection(_context);

            #endregion

            #region CommandsIni

            DeletePairCommand = new RelayCommand<IPair>(DeleteIPairItem);
            AddPairCommand = new RelayCommand<IPair>(AddPairItem);
            EditPairCommand = new RelayCommand<IPair>(EditPairItem);

            LoadMailCommand = new RelayCommand(LoadMail);
            SaveMailCommand = new RelayCommand<Mail>(SaveMail);
            DeleteMailCommand = new RelayCommand<Mail>(DeleteMail);
            NewMailCommand = new RelayCommand(NewMail);

            SendCommand = new RelayCommand<string>(Send);

            DeleteSendedCommand = new RelayCommand<Sended>(DeleteSended);

            #endregion

            #region EventHandlers

            _receiversViewSource = new CollectionViewSource() { Source = _receivers };
            _receiversViewSource.Filter += new FilterEventHandler(OnSendersCollectionViewSourceFilter);

            SendingMails.AllSended += OnAllSended;

            #endregion

        }

        #region Methods

        private void DeleteIPairItem(IPair item)
        {
            List<string> errList= new List<string>();

            if (item != null)
            {
                ForObsCollection(item.GetType())?.DeleteIPair(item);
            }
            else errList.Add($"{item.ClassName} дл€ удалени€ не выбран.");

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, OtherConstants.ErrorWindowTitle);
                window.ShowDialog();
            }
        }

        private void EditPairItem(IPair item)
        {
            List<string> errList = new List<string>();

            if (item != null)
            {
                AEPairItemWindow AEWindow = new AEPairItemWindow();
                AEWindow.Title = "–едактировать";
                AEWindow.Item = item;
                AEWindow.ShowDialog();
                //приходит null или валидные данные
                if (AEWindow.Item != null)
                {
                    ForObsCollection(AEWindow.Item.GetType())?.NotifyPairModified(AEWindow.Item);
                }
            }
            else errList.Add($"{item.ClassName} дл€ редактировани€ не выбран.");

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, OtherConstants.ErrorWindowTitle);
                window.ShowDialog();
            }
        }

        private void AddPairItem(IPair item)
        {
            AEPairItemWindow AEWindow = new AEPairItemWindow();
            AEWindow.Title = "ƒобавить";

            AEWindow.Item = (IPair)Activator.CreateInstance(item.GetType());

            AEWindow.ShowDialog();

            if (AEWindow.Item != null) 
            {
                ForObsCollection(AEWindow.Item.GetType())?.AddIPair(AEWindow.Item);
            }
        }

        private void SaveMail(Mail mail)
        {
            List<string> errList = new List<string>();

            if (SelectedMail != null)
            {
                if (!string.IsNullOrEmpty(SelectedMail.Topic))
                {
                    if (mail.Created.Equals(new DateTime())) mail.Created = DateTime.Now;
                    if (mail.Id == -1) SelectedMail = Mails.AddMail(SelectedMail);
                    else Mails.NotifyMailModified(SelectedMail);
                }
                else errList.Add($"{ClassNamesConstants.Topic} письма не должна быть пустой.");
            }
            else errList.Add("—оздайте новое письмо.");
            
            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, OtherConstants.ErrorWindowTitle);
                window.ShowDialog();
            }
        }

        private void NewMail()
        {
            SelectedMail = new Mail(){Id=-1};//Id дл€ того, чтобы определ€ть по-простому, что это новое письмо. 
        }

        private void LoadMail()
        {
            LoadMailsWindow lmw= new LoadMailsWindow(Mails);
            lmw.ShowDialog();
            if(lmw.Selected!=null) SelectedMail = lmw.Selected;
        }

        private void DeleteMail(Mail mail)
        {
            List<string> errList = new List<string>();

            if (mail!=null)
            {
                Mails.DeleteMail(mail);
            }
            else errList.Add($"{ClassNamesConstants.MailClassName} дл€ удалени€ не выбран.");

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, OtherConstants.ErrorWindowTitle);
                window.ShowDialog();
            }
        }

        private void Send(string dtSendTime)
        {
            List<string> errList = new List<string>();
            List<Receiver> tempReceivers = new List<Receiver>();
            foreach (Receiver receiver in Receivers)
            {
                if (receiver.IsMailing) tempReceivers.Add(receiver);
            }

            if (!((SelectedMail == null) || (string.IsNullOrEmpty(SelectedMail.Topic)) || SelectedSender == null ||
                  SelectdSMTP == null || tempReceivers.Count == 0))
            {
                Sended sn = new Sended
                {
                    Mail = SelectedMail,
                    Created = DateTime.Now,
                    Sender = (Sender) SelectedSender,
                    SMTP = (SMTP) SelectdSMTP,
                    SendedReceivers = new ObservableCollection<SendedReceiver>()
                };

                foreach (Receiver receiver in tempReceivers)
                {
                    SendedReceiver sr = new SendedReceiver
                    {
                        Receiver = receiver,
                        Sended = sn
                    };
                    sn.SendedReceivers.Add(sr);
                }

                if (string.IsNullOrEmpty(dtSendTime))
                {
                    SendingMails.Send(sn);
                }
                else
                {
                    DateTime sendTime = DateTime.Parse(dtSendTime);
                    if (sendTime > DateTime.Now)
                    {
                        Task.Run(() =>
                        {
                            Thread.Sleep(sendTime - DateTime.Now);
                            System.Windows.Application.Current.Dispatcher.Invoke(()=>SendingMails.Send(sn));
                        });
                    }
                    else
                    {
                        errList.Add("¬рем€ отправки должно быть позже.");
                    }
                }
                //происходит ивент, о том, что ошибки загрузились и отправка закончилась.  OnAllSended

            }
            else
            {
                if ((SelectedMail == null) || (string.IsNullOrEmpty(SelectedMail.Topic)))
                    errList.Add("¬ыберите или создайте письмо. Ќовое письмо об€зательно должно иметь тему.");

                if (SelectedSender == null) errList.Add($"{ClassNamesConstants.SenderClassName} на странице ‘ормирование рассылки не выбран.");

                if (SelectdSMTP == null) errList.Add($"{ClassNamesConstants.SMTPClassName} на странице ‘ормирование рассылки не выбран.");

                if (tempReceivers.Count == 0) errList.Add($"Ќи один {ClassNamesConstants.ReceiverClassName} на странице ‘ормирование рассылки не выбран.");
            }

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, OtherConstants.ErrorWindowTitle);
                window.ShowDialog();
            }
        }

        private void DeleteSended(Sended item)
        {
            List<string> errList = new List<string>();

            if (item != null)
            {
                History.DeleteSended(item);
            }
            else errList.Add($"{ClassNamesConstants.SendedClassName} дл€ удалени€ не выбрана.");

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, OtherConstants.ErrorWindowTitle);
                window.ShowDialog();
            }
        }

        #endregion

        #region MainViewModel Methods

        private PairObsCollection ForObsCollection(Type type)
        {
            //switch по типам не работает, почему то. 
            if (type == typeof(Sender)) return Senders;
            if (type == typeof(Receiver)) return Receivers;
            if (type == typeof(SMTP)) return SMTPs;
            return null;
        }

        private void OnAllSended(Dictionary<SendedReceiver, string> errDic, Sended sn)
        {
            List<string> errList = new List<string>();

            foreach (KeyValuePair<SendedReceiver, string> pair in errDic)
            {
                sn.SendedReceivers.Remove(pair.Key);
                errList.Add($"ѕисьмо дл€ {pair.Key.Receiver.Key}  не было доставлено потому, что {pair.Value}.");
            }

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, OtherConstants.ErrorWindowTitle);
                window.ShowDialog();
            }

            History.AddSended(sn);

        }

        #endregion
    }
}