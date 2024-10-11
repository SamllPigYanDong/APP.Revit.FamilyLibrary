using System;
using System.Globalization;
using System.Windows.Data;

namespace Revit.Application.Styles.Converter
{
    public class ConvMultiply : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double num = 1.0;
            foreach (object obj in values)
            {
                double num2=0;
                bool flag;
                if (obj is double)
                {
                    num2 = (double)obj;
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                bool flag2 = flag;
                if (flag2)
                {
                    num *= num2;
                }
            }
            return num;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
