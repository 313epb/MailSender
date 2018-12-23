using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            this.Close();
        }
    }
}
