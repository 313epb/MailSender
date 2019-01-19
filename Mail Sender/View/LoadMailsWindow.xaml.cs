using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MailSender.Domain.Entities;
using Mail_Sender.Annotations;

namespace Mail_Sender.View
{
    /// <summary>
    /// Interaction logic for LoadMailsWindow.xaml
    /// </summary>
    public partial class LoadMailsWindow : Window, INotifyPropertyChanged
    {
        private Mail _selected;

        public Mail Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }


        private ObservableCollection<Mail> _mails;

        public LoadMailsWindow(ObservableCollection<Mail> mails)
        {

            _mails = mails;
            InitializeComponent();
            foreach (Mail mail in _mails)
            {
                RadioButton rb = new RadioButton
                {
                    Name = "rbMail" + mail.Id,
                    Content = mail.Topic,
                    GroupName = "Mails",
                    Margin = new Thickness(5, 3, 3, 3)
                };
                rb.Checked += RbChecked_Handler;
                spTopicPanel.Children.Add(rb);
            }
        }


        private void RbChecked_Handler(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton) sender;
            Selected = _mails.FirstOrDefault(m => m.Topic == pressed.Content);
            delButton.CommandParameter = Selected;       //костыль
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DelButton_OnClick(object sender, RoutedEventArgs e)
        {
            List<RadioButton> radioButtons = new List<RadioButton>();
            WalkLogicalTree(radioButtons, spTopicPanel);
            bool commandParametertoNull = true; //следствие костыля

            foreach (RadioButton rbutton in radioButtons)
            {
                if ((bool)rbutton.IsChecked)
                {
                    spTopicPanel.Children.Remove(rbutton);
                    commandParametertoNull = false;
                }
            }

            if (commandParametertoNull)
            {
                delButton.CommandParameter = null;
            }

            Selected = null;
        }

        private void WalkLogicalTree(List<RadioButton> radioButtons, object parent)
        {
            DependencyObject doParent = parent as DependencyObject;
            if (doParent == null) return;
            foreach (object child in LogicalTreeHelper.GetChildren(doParent))
            {
                if (child is RadioButton)
                {
                    radioButtons.Add(child as RadioButton);
                }

                WalkLogicalTree(radioButtons, child);
            }
        }
    }
}
