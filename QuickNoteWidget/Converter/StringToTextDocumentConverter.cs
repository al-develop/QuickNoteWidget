using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ICSharpCode.AvalonEdit.Document;

namespace QuickNoteWidget.Converter
{
    public class StringToTextDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TextDocument doc = new TextDocument();
            doc.Text = (string)value;
            return doc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var doc = (TextDocument) value;
            var txt = doc.Text;
            return txt;
        }
    }
}
