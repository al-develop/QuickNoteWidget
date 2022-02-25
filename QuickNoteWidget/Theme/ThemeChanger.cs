using System;
using System.Windows;
using ControlzEx.Theming;

namespace QuickNoteWidget.Theme
{
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