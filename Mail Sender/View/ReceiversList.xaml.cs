using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MailSender.Domain.Entities;
using Mail_Sender.ViewModel;

namespace Mail_Sender.View
{
    /// <summary>
    /// Interaction logic for ReceiversList.xaml
    /// </summary>
    public partial class ReceiversList : UserControl
    {

        public Receiver SelectedReceiver
        {
            get { return (Receiver)GetValue(SelectedReceiverProperty); }
            set { SetValue(SelectedReceiverProperty, value); }
        }

        public static readonly DependencyProperty SelectedReceiverProperty =
            DependencyProperty.Register("SelectedReceiver", typeof(Receiver),
                typeof(AEDClassItemControl), new PropertyMetadata(default(Receiver)));

        public ReceiversList()
        {
            InitializeComponent();
        }


        private void DgReceivers_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedReceiver = (Receiver) dgReceivers.SelectedItem;
        }
    }
}
