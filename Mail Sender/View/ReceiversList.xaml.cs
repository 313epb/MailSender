﻿using System;
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
        public string ClassName
        {
            get { return (string)GetValue(ClassNameProperty); }
            set { SetValue(ClassNameProperty, value); }
        }

        public static readonly DependencyProperty ClassNameProperty =
            DependencyProperty.Register("ClassName", typeof(string),
                typeof(ReceiversList), new PropertyMetadata(""));


        public ReceiversList()
        {
            InitializeComponent();
        }



    }
}
