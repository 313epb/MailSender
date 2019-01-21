using GalaSoft.MvvmLight;
using MailSender.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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

        private MailObsCollection _mails = new MailObsCollection(DataContext);

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

        private IPairObsCollection _senders= new IPairObsCollection(ClassNamesConstants.SenderClassName,DataContext);

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

        private IPair _objSender = new Sender();

        public IPair ObjSender { get => _objSender; set => _objSender = value; }

        #endregion

        #region SMTPs

        public IPair SelectdSMTP { get; set; }

        private IPairObsCollection _smtps= new IPairObsCollection(ClassNamesConstants.SMTPClassName,DataContext);

        public IPairObsCollection SMTPs
        {
            get => _smtps;
            set => _smtps = value;
        }

        private readonly string _SMTPSClassName = MailSender.Domain.Constants.ClassNamesConstants.SMTPClassName;
        public string SMTPClassName => _SMTPSClassName;

        private IPair _objSmtp = new SMTP();

        public IPair OBjSmtp { get => _objSmtp; set => _objSmtp = value; }

        #endregion

        #region Receivers

        private IPairObsCollection _receivers= new IPairObsCollection(ClassNamesConstants.ReceiverClassName, DataContext);

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
            if (!re.Value.Contains(_filterName))
                e.Accepted = false;
        }

        private readonly string _receiverClassName = MailSender.Domain.Constants.ClassNamesConstants.ReceiverClassName;
        public string ReceiverClassName => _receiverClassName;

        private IPair _objReceiver = new Receiver();

        public IPair OBjReceiver { get => _objReceiver; set => _objReceiver = value; }

        #endregion

        #region History

        private SendedObsCollection _history= new SendedObsCollection(DataContext);

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

        public RelayCommand<IPair> AddPairCommand{ get; set; }

        public RelayCommand<IPair> EditPairCommand { get; set; }

        public RelayCommand LoadMailCommand { get; set; }

        public RelayCommand SaveMailCommand { get; set; }

        public RelayCommand<Mail> DeleteMailCommand { get; set; }

        public RelayCommand SendNowCommand { get; set; }

        public RelayCommand SendLaterCommand { get; set; }

        public RelayCommand<Sended> DeleteSendedCommand { get; set; }

        #endregion

        public static MailSenderContext DataContext = MailSenderContext.Instance;

        public MainViewModel()
            {
                #region CommandsIni

                DeletePairCommand = new RelayCommand<IPair>(DeleteIPairItem);
                AddPairCommand= new RelayCommand<IPair>(AddPairItem);
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
                SendingMails.AllSended+=OnAllSended;
            }

        #region Methods

        private void DeleteIPairItem(IPair item)
        {
            List<string> errList= new List<string>();
            
            if (item != null)
            {
                ForObsCollection(item.GetType())?.DeleteIPair(item);
            }
            else errList.Add("Не выбран объект для удаления");

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList,"Обнаружена ошибка.");
                window.ShowDialog();
            }
        }

        private void EditPairItem(IPair item)
        {
            List<string> errList = new List<string>();

            if (item!=null)
            {
                AEPairItemWindow AEWindow = new AEPairItemWindow();
                AEWindow.Title = "Редактировать";
                AEWindow.Item = item;
                AEWindow.ShowDialog();
                //приходит null или валидные данные
                if (AEWindow.Item!=null)
                {
                    ForObsCollection(item.GetType())?.NotifyPairModified(item);
                }
            }
            else errList.Add("Выберите значение для редактирования");

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, "Обнаружена ошибка.");
                window.ShowDialog();
            }
        }

        private void AddPairItem(IPair item)
        {
            AEPairItemWindow AEWindow = new AEPairItemWindow();
            AEWindow.Title = "Добавить";

            AEWindow.Item = (IPair)Activator.CreateInstance(item.GetType());

            AEWindow.ShowDialog();

            if (AEWindow.Item != null) 
            {
                ForObsCollection(AEWindow.Item.GetType())?.AddIPair(AEWindow.Item);
            }
        }

        private void SaveMail()
        {
            List<string> errList = new List<string>();

            if (SelectedMail!=null)
            {
                if (!string.IsNullOrEmpty(SelectedMail.Topic))
                {
                    Mail temp;
                    if ((temp = Mails.FirstOrDefault(m => m.Topic == SelectedMail.Topic)) != null)
                    {
                        temp.Content = SelectedMail.Content;
                        Mails.NotifyMailModified(temp);
                    }
                    else
                    {
                        SelectedMail.Created = DateTime.Now;
                        Mails.AddMail(SelectedMail);
                    }

                    SelectedMail = new Mail
                    {
                        Topic = SelectedMail.Topic,
                        Content = SelectedMail.Content,
                        Created = new DateTime(),
                        IsHTML = SelectedMail.IsHTML
                    };
                }
                else errList.Add("Тема письма не должна быть пустой.");
            }
            else errList.Add("Создайте новое письмо. (этой ошибки не должно быть)");

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, "Обнаружена ошибка.");
                window.ShowDialog();
            }
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
            else errList.Add("Вы не выбрали письмо для удаления.");

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, "Обнаружена ошибка.");
                window.ShowDialog();
            }
        }

        private void SendNow()
        {
            List<string> errList = new List<string>();
            List<Receiver> tempReceivers=new List<Receiver>();
            foreach (Receiver receiver in Receivers)
            {
                if (receiver.IsMailing) tempReceivers.Add(receiver); 
            }

            if (!((SelectedMail == null) || (string.IsNullOrEmpty(SelectedMail.Topic)) || SelectedSender == null|| SelectdSMTP == null|| tempReceivers.Count==0))
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

                SendingMails.Send(sn);
                //происходит ивент, о том, что ошибки загрузились и отправка закончилась.  OnAllSended

            }
            else
            {
                if ((SelectedMail == null) || (string.IsNullOrEmpty(SelectedMail.Topic))) errList.Add("Выберите или создайте письмо. Новое письмо обязательно должно иметь тему.");

                if (SelectedSender == null) errList.Add("Вы не выбрали отправителя на странице Рассылка");

                if (SelectdSMTP == null) errList.Add("Вы не выбрали SMTP сервер на странице Рассылка");

                if (tempReceivers.Count == 0) errList.Add("Вы не выбрали получателей на странице Рассылка");

                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, "Обнаружена ошибка.");
                window.ShowDialog();
            }
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
            List<string> errList = new List<string>();

            if (item!=null)
            {
                History.DeleteSended(item);
            }
            else errList.Add("Вы не выбрали рассылку для удаления.");

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, "Обнаружена ошибка.");
                window.ShowDialog();
            }
        }

        #endregion

        #region MainViewModel Methods

        private IPairObsCollection ForObsCollection(Type type)
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
                errList.Add($"Письмо для {pair.Key.Receiver.Key}  не было доставлено потому, что {pair.Value}.");
            }

            if (errList.Count != 0)
            {
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList, "Обнаружена ошибка.");
                window.ShowDialog();
            }

            History.AddSended(sn);

        }

        #endregion
    }
}