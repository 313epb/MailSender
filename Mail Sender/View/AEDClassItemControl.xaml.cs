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
using MailSender.Domain.Entities.Base;
using MailSender.Domain.Entities.Base.Interface;

namespace Mail_Sender.View
{
    /// <summary>
    /// Interaction logic for AEDClassItemControl.xaml
    /// </summary>
    public partial class AEDClassItemControl : UserControl
    {
        /// <summary>
        /// Имя класса для отображения
        /// </summary>
        public string ClassName
        {
            get { return (string)GetValue(ClassNameProperty); }
            set { SetValue(ClassNameProperty, value); }
        }

        public static readonly DependencyProperty ClassNameProperty =
            DependencyProperty.Register("ClassName", typeof(string),
                typeof(AEDClassItemControl), new PropertyMetadata(""));

        /// <summary>
        /// Получаем объект Sender или SMTP.
        /// Sender - key = email value=password
        /// SMTP-key=SMTP value=port
        /// </summary>
        public List<IPair> ItemsList
        {
            get { return (List<IPair>)GetValue(ItemListProperty); }
            set { SetValue(ItemListProperty, value); }
        }

        public static readonly DependencyProperty ItemListProperty =
            DependencyProperty.Register("ItemsList", typeof(List<IPair>),
                typeof(AEDClassItemControl), new PropertyMetadata(default(List<IPair>)));

        public AEDClassItemControl()
        {
            InitializeComponent();
            MainGrid.DataContext = this;
        }
    }
}
