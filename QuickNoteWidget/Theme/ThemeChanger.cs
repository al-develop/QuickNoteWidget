using System;
using System.Windows;
using ControlzEx.Theming;

namespace QuickNoteWidget.Theme
{
    /// <summary>
    /// To keep the MainWindowViewModel agnostic of the Window, 
    /// this Helper class can be used to change the theme from the ViewModel
    /// Default Values are
    ///     Accent : Cyan | Theme : Light
    /// </summary>
    public static class ThemeChanger
    {
        public static void ChangeTheme(string accent, string theme)
        {
            if (String.IsNullOrEmpty(accent))
                accent = "Cyan";


            if (String.IsNullOrEmpty(theme))
                theme = "Light";


            string name = String.Concat(theme, ".", accent);
            ThemeManager.Current.ChangeTheme(Application.Current, name);
        }
    }
}