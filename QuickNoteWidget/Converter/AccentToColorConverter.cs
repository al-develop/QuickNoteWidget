using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using MahApps.Metro;

namespace QuickNoteWidget.Converter
{
    public class AccentToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Accent accent = (Accent)value;
            switch (accent.Name)
            {
                case "Red": return (Color)ThemeManager.GetAccent("Red").Resources["AccentColor"];
                case "Green": return (Color)ThemeManager.GetAccent("Green").Resources["AccentColor"];
                case "Blue": return (Color)ThemeManager.GetAccent("Blue").Resources["AccentColor"];
                case "Purple": return (Color)ThemeManager.GetAccent("Purple").Resources["AccentColor"];
                case "Orange": return (Color)ThemeManager.GetAccent("Orange").Resources["AccentColor"];
                case "Lime": return (Color)ThemeManager.GetAccent("Lime").Resources["AccentColor"];
                case "Emerald": return (Color)ThemeManager.GetAccent("Emerald").Resources["AccentColor"];
                case "Teal": return (Color)ThemeManager.GetAccent("Teal").Resources["AccentColor"];
                case "Cyan": return (Color)ThemeManager.GetAccent("Cyan").Resources["AccentColor"];
                case "Cobalt": return (Color)ThemeManager.GetAccent("Cobalt").Resources["AccentColor"];
                case "Indigo": return (Color)ThemeManager.GetAccent("Indigo").Resources["AccentColor"];
                case "Violet": return (Color)ThemeManager.GetAccent("Violet").Resources["AccentColor"];
                case "Pink": return (Color)ThemeManager.GetAccent("Pink").Resources["AccentColor"];
                case "Magenta": return (Color)ThemeManager.GetAccent("Magenta").Resources["AccentColor"];
                case "Crimson": return (Color)ThemeManager.GetAccent("Crimson").Resources["AccentColor"];
                case "Amber": return (Color)ThemeManager.GetAccent("Amber").Resources["AccentColor"];
                case "Yellow": return (Color)ThemeManager.GetAccent("Yellow").Resources["AccentColor"];
                case "Brown": return (Color)ThemeManager.GetAccent("Brown").Resources["AccentColor"];
                case "Olive": return (Color)ThemeManager.GetAccent("Olive").Resources["AccentColor"];
                case "Steel": return (Color)ThemeManager.GetAccent("Steel").Resources["AccentColor"];
                case "Mauve": return (Color)ThemeManager.GetAccent("Mauve").Resources["AccentColor"];
                case "Taupe": return (Color)ThemeManager.GetAccent("Taupe").Resources["AccentColor"];
                case "Sienna": return (Color)ThemeManager.GetAccent("Sienna").Resources["AccentColor"];
                default: return (Color)ThemeManager.GetAccent("Cyan").Resources["AccentColor"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
