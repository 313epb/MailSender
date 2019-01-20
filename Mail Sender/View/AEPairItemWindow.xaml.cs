using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;
using Mail_Sender.Annotations;

namespace Mail_Sender.View
{
    /// <summary>
    /// Interaction logic for AEPairItemWindow.xaml
    /// </summary>
    public partial class AEPairItemWindow : Window,INotifyPropertyChanged
    {
        private IPair item;
        public IPair Item { get=>item;
            set
            {
                item = value; 
                OnPropertyChanged("item");
            }
        }

        private bool save { get; set; } = false;

        public AEPairItemWindow()
        {
            InitializeComponent();
           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            save = true;
            this.Close();
        }

        private bool AllValid(DependencyObject obj)
        {
            return !Validation.GetHasError(obj) &&
                   LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>().All(AllValid);

        }



        private void AEPairItemWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (save)
            {
                if (!AllValid(this))
                {
                    List<string> errList = new List<string>()
                    {
                        "Не все поля валидны."
                    };
                    MyMessageBoxWindow errWindow = new MyMessageBoxWindow(errList, "Обнаружена ошибка.");
                    errWindow.ShowDialog();
                    Item = null;
                }
            }
            else
            {
                Item = null;
            }
        }
    }
}
