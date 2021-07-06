using System;
using System.Windows;
using ControlzEx.Theming;

namespace QuickNoteWidget
{
    public static class ThemeChanger
    {
        public static void ChangeTheme(string accent, string theme)
        {
            if (String.IsNullOrEmpty(accent))
                accent = "Cyan";

            if (String.IsNullOrEmpty(theme))
                theme = "Light";


            string name = string.Concat(theme, ".", accent);
            ThemeManager.Current.ChangeTheme(Application.Current, name);
        }
    }
}
