using GalaSoft.MvvmLight;
using MailSender.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using MailSender.Domain.Entities.Base.Interface;
using MailSender.Domain.Constants;
using Mail_Sender.View;
using System.Windows.Data;
using MailSender.Domain.Entities.Base;
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

        public RelayCommand<string> AddPairCommand{ get; set; }

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
            List<string> errList= new List<string>();

            if (item != null)
            {
                if (item.GetType() == typeof(Sender))
                {
                    Senders.DeleteIPair(item);
                }

                if (item.GetType() == typeof(Receiver))
                {
                    Receivers.DeleteIPair(item);
                }

                if (item.GetType() == typeof(SMTP))
                {
                    SMTPs.DeleteIPair(item);
                }
            }
            else
            {
                errList.Add("Не выбран объект для удаления");
                MyMessageBoxWindow window = new MyMessageBoxWindow(errList);
                window.ShowDialog();
            }
        }

        private void EditPairItem(IPair item)
        {
            if (item!=null)
            {
                PairEntity editablPairEntity= new PairEntity()
                {
                    Id = item.Id,
                    Key = item.Key,
                    Value = item.Value,
                    
                };
                AEPairItemWindow AEWindow = new AEPairItemWindow();
                AEWindow.Title = "Редактировать";
                AEWindow.Item = editablPairEntity;
                AEWindow.ShowDialog();
                editablPairEntity = (PairEntity)AEWindow.Item; //надеемся, что из окна приходит валидный объект, а если не валидный, то будет null
                if (editablPairEntity!=null)
                {
                    if (item.GetType() == typeof(Sender))
                    {
                        Senders.NotifyPairModified(Sender.ConvertFromIPair(editablPairEntity));
                    }

                    if (item.GetType() == typeof(Receiver))
                    {
                        Receivers.NotifyPairModified(Receiver.ConvertFromIPair(editablPairEntity));
                    }

                    if (item.GetType() == typeof(SMTP))
                    {
                        SMTPs.NotifyPairModified(SMTP.ConvertFromIPair(editablPairEntity));
                    }
                }
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
                temp.Content = SelectedMail.Content;
            }
            else
            {
                SelectedMail.Topic = SelectedMail.Topic.ToString();
                SelectedMail.Created = DateTime.Now;
                Mails.AddMail(SelectedMail);
            }

            SelectedMail = new Mail
            {
                Topic = SelectedMail.Topic?.ToString(),
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
            if ((SelectedMail == null) || (string.IsNullOrEmpty(SelectedMail.Topic)))
            {
                MessageBox.Show("Топик или письмо путсые");
            }

            else
            {
                if (SelectedSender == null)
                {
                    MessageBox.Show("Вы не выбрали отправителя");}
                else
                {
                    if (SelectdSMTP == null)
                    {
                        MessageBox.Show("Вы не выбрали SMTP сервер");
                    }
                    else
                    {
                        Sended sn = new Sended
                        {
                            Mail = SelectedMail,
                            Created = DateTime.Now,
                            Sender = (Sender) SelectedSender,
                            SMTP = (SMTP) SelectdSMTP,
                            SendedReceivers = new ObservableCollection<SendedReceiver>()
                        };

                        foreach (Receiver receiver in Receivers)
                        {
                            if (receiver.IsMailing)
                            {
                                SendedReceiver sr = new SendedReceiver
                                {
                                    Receiver = receiver,
                                    Sended = sn
                                };
                                sn.SendedReceivers.Add(sr);
                            }
                        }

                        if (sn.SendedReceivers.Count == 0)
                        {
                            MessageBox.Show("Вы не выбрали получателей");
                        }
                        else
                        {
                            SendingMails.Send(sn);
                            History.AddSended(sn);
                        }
                    }
                }
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
            History.DeleteSended(item);
        }

        #endregion




    }
}