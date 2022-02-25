using System;
using System.Windows;
using ControlzEx.Theming;

namespace QuickNoteWidget.Theme
{
    /// <summary>
    /// To keep the MainWindowViewModel agnostic of the Window, 
    /// this Helper class can be usd to change the theme from the ViewModel
    /// </summary>
    public static class ThemeChanger
    {
        public static void ChangeTheme(string accent, string theme)
        {
            if (string.IsNullOrEmpty(accent))
                accent = "Cyan";


            if (string.IsNullOrEmpty(theme))
                theme = "Light";


            string name = string.Concat(theme, ".", accent);
            ThemeManager.Current.ChangeTheme(Application.Current, name);
        }
    }
}