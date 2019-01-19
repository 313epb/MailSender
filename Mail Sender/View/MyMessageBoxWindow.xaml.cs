using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Mail_Sender.View
{
    /// <summary>
    /// Interaction logic for MyMessageBoxWindow.xaml
    /// </summary>
    public partial class MyMessageBoxWindow : Window
    {
        internal List<string> LastErrorList { get; set; }= new List<string>();

        public MyMessageBoxWindow(List<string> errList)
        {
            InitializeComponent();
            foreach (string err in errList)
            {
                errorPanel.Children.Add(new TextBox
                {
                    Text = err,
                    IsReadOnly = true,
                    TextWrapping = TextWrapping.Wrap
                });
                LastErrorList.Add(err);
            }
        }

        private void ButtonReady_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
