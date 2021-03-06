﻿using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace Mail_Sender.Converters
{
    public class TabSizeConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter,System.Globalization.CultureInfo culture)
        {
            TabControl tabControl = values[0] as TabControl;
            double width = tabControl.ActualWidth / tabControl.Items.Count;

            return (width <= 1) ? 0 : (width-5);//5 - Опытно подобранная цифра, из-за маржинов, указанных в стиле.

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}