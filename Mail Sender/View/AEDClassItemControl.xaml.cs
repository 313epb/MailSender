using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MailSender.Domain.Entities.Base.Interface;

namespace Mail_Sender.View
{
    /// <summary>
    /// Interaction logic for AEDClassItemControl.xaml
    /// </summary>
    public partial class AEDClassItemControl : UserControl
    {
        public IPair SelectedIPair
        {
            get { return (IPair)GetValue(SelectedIPairProperty); }
            set { SetValue(SelectedIPairProperty, value); }
        }

        public static readonly DependencyProperty SelectedIPairProperty =
            DependencyProperty.Register("SelectedIPair", typeof(IPair),
                typeof(AEDClassItemControl), new PropertyMetadata(default(IPair)));

        /// <summary>
        /// Имя класса для отображения
        /// </summary>

        public IPair ObjectIPair
        {
            get { return (IPair)GetValue(ObjectIPairProperty); ; }
            set { SetValue(ObjectIPairProperty, value); }
        }

        public static readonly DependencyProperty ObjectIPairProperty =
            DependencyProperty.Register("ObjectIPair", typeof(IPair),
                typeof(AEDClassItemControl), new PropertyMetadata(default(IPair)));

        /// <summary>
        /// Получаем объект Sender или SMTP.
        /// Sender - key = email value=password
        /// SMTP-key=SMTP value=port
        /// </summary>
        public ObservableCollection<IPair> ItemsList
        {
            get { return (ObservableCollection<IPair>)GetValue(ItemListProperty); }
            set { SetValue(ItemListProperty, value); }
        }

        

        public static readonly DependencyProperty ItemListProperty =
            DependencyProperty.Register("ItemsList", typeof(ObservableCollection<IPair>),
                typeof(AEDClassItemControl), new PropertyMetadata(default(ObservableCollection<IPair>)));

        public AEDClassItemControl()
        {
            InitializeComponent();
            MainGrid.DataContext = this;
        }

        private void CbSelect_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedIPair = (IPair)cbSelect.SelectedItem;
        }
    }
}
