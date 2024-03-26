﻿using System;
using System.Windows;
using System.Windows.Data;

namespace Revit.Application.Converter
{

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible;
        }
    }
}
