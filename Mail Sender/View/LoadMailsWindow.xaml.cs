using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MailSender.Domain.Entities;

namespace Mail_Sender.View
{
    /// <summary>
    /// Interaction logic for LoadMailsWindow.xaml
    /// </summary>
    public partial class LoadMailsWindow : Window
    {
        private Mail _selected;

        public Mail Selected
        {
            get => _selected;
            set
            {
                _selected = value;

            }
        }

        private ObservableCollection<Mail> _mails;
        
        public LoadMailsWindow(ObservableCollection<Mail> mails)
        {
            _mails = mails;
            InitializeComponent();
            foreach (Mail mail in _mails)
            {
                RadioButton rb= new RadioButton
                {
                    Name = "rbMail"+mail.Id,
                    Content = mail.Topic,
                    GroupName = "Mails",
                    Margin = new Thickness(5,3,3,3)
                };
                rb.Checked += RbChecked_Handler;
                spTopicPanel.Children.Add(rb);
            }
        }

        private void RbChecked_Handler(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            Selected = _mails.FirstOrDefault(m => m.Content == pressed.Content);
        }
    }
}
