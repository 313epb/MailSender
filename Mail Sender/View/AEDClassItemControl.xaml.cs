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

namespace Mail_Sender.View
{
    /// <summary>
    /// Interaction logic for AEDClassItemControl.xaml
    /// </summary>
    public partial class AEDClassItemControl : UserControl
    {

        public String ClassName
        {
            get { return (String)GetValue(ClassNameProperty); }
            set { SetValue(ClassNameProperty, value); }
        }

        public static readonly DependencyProperty ClassNameProperty =
            DependencyProperty.Register("ClassName", typeof(string),
                typeof(AEDClassItemControl), new PropertyMetadata(""));

        //public List ItemsList
        //{
        //    get { return (List)GetValue(ItemListProperty); }
        //    set { SetValue(ItemListProperty, value); }
        //}

        //public static readonly DependencyProperty ItemListProperty =
        //    DependencyProperty.Register("ClassName", typeof(string),
        //        typeof(AEDClassItemControl), new PropertyMetadata(""));

        public AEDClassItemControl()
        {
            InitializeComponent();
        }
    }
}
