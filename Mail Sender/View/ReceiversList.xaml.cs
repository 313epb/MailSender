using System.Windows;
using System.Windows.Controls;
using MailSender.Domain.Entities.Base.Interface;

namespace Mail_Sender.View
{
    /// <summary>
    /// Interaction logic for ReceiversList.xaml
    /// </summary>
    public partial class ReceiversList : UserControl
    {
        
        public IPair ObjectIPair
        {
            get { return (IPair)GetValue(ObjectIPairProperty); ; }
            set { SetValue(ObjectIPairProperty, value); }
        }

        public static readonly DependencyProperty ObjectIPairProperty =
            DependencyProperty.Register("ObjectIPair", typeof(IPair),
                typeof(ReceiversList), new PropertyMetadata(default(IPair)));

        public ReceiversList()
        {
            InitializeComponent();
        }



    }
}
