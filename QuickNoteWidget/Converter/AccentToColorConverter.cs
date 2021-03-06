﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using ControlzEx.Theming;

namespace QuickNoteWidget.Converter
{
    [Obsolete("with mahapps 2.0 and later, this converter is not necessary anymore")]
    public class AccentToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string accent = (string)value;
            return (string)ThemeManager.Current.BaseColors.FirstOrDefault(f => f == accent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
