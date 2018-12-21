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
    /// Interaction logic for AEReceivers.xaml
    /// </summary>
    public partial class AEReceivers : UserControl
    {
        public AEReceivers()
        {
            InitializeComponent();
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
