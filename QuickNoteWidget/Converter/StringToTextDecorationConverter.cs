using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace QuickNoteWidget.Converter
{
    public class StringToTextDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;

            string textDecoration = value.ToString();


            if (textDecoration == "Strikethrough")
                return TextDecorations.Strikethrough;

            else if (textDecoration == "Underline")
                return TextDecorations.Underline;

            else if (textDecoration == "OverLine")
                return TextDecorations.OverLine;

            else if (textDecoration == "Baseline")
                return TextDecorations.Baseline;


            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}