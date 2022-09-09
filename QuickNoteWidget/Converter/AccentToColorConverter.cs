using System;
using System.Globalization;
using System.Windows.Data;

namespace QuickNoteWidget.Converter
{
    public class AccentToColorConverter : IValueConverter
    {
        public string Convert(string value)
        {
            return Convert(value, null, null, null).ToString();
        }


        /// <summary>
        /// Convert MahApps.ColorScheme to according Hex values
        /// Hex values taken from MahApps Source Code
        /// https://github.com/MahApps/MahApps.Metro
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string accent = (string)value;
            switch (accent)
            {
                case "Red": return "#FFE51400";
                case "Green": return "#FF60A917";
                case "Blue": return "#FF0078D7";
                case "Purple": return "#FF6459DF";
                case "Orange": return "#FFFA6800";
                case "Lime": return "#FFA4C400";
                case "Emerald": return "#FF008A00";
                case "Teal": return "#FF00ABA9";
                case "Cyan": return "#FF1BA1E2";
                case "Cobalt": return "#FF0050EF";
                case "Indigo": return "#FF6A00FF";
                case "Violet": return "#FFAA00FF";
                case "Pink": return "#FFF472D0";
                case "Magenta": return "#FFD80073";
                case "Crimson": return "#FFA20025";
                case "Amber": return "#FFF0A30A";
                case "Yellow": return "#FFFEDE06";
                case "Brown": return "#FF825A2C";
                case "Olive": return "#FF6D8764";
                case "Steel": return "#FF647687";
                case "Mauve": return "#FF76608A";
                case "Taupe": return "#FF87794E";
                case "Sienna": return "#FFA0522D";
                default: return "Cyan";
            }
        }
        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string accent = (string)value;
            switch (accent)
            {
                case "#FFE51400": return "Red";
                case "#FF60A917": return "Green";
                case "#FF0078D7": return "Blue";
                case "#FF6459DF": return "Purple";
                case "#FFFA6800": return "Orange";
                case "#FFA4C400": return "Lime";
                case "#FF008A00": return "Emerald";
                case "#FF00ABA9": return "Teal";
                case "#FF1BA1E2": return "Cyan";
                case "#FF0050EF": return "Cobalt";
                case "#FF6A00FF": return "Indigo";
                case "#FFAA00FF": return "Violet";
                case "#FFF472D0": return "Pink";
                case "#FFD80073": return "Magenta";
                case "#FFA20025": return "Crimson";
                case "#FFF0A30A": return "Amber";
                case "#FFFEDE06": return "Yellow";
                case "#FF825A2C": return "Brown";
                case "#FF6D8764": return "Olive";
                case "#FF647687": return "Steel";
                case "#FF76608A": return "Mauve";
                case "#FF87794E": return "Taupe";
                case "#FFA0522D": return "Sienna";
                default: return "#FF1BA1E2";
            }
        }
    }
}