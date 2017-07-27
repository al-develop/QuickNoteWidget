using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro;

namespace QuickNoteWidget
{
    public static class ThemeChanger
    {
        public static void ChangeTheme(Accent accent, AppTheme theme)
        {
            Accent defaultAccent = ThemeManager.GetAccent("Cyan");
            AppTheme defaultAppTheme = ThemeManager.GetAppTheme("BaseLight");
            ThemeManager.ChangeAppStyle(Application.Current, accent ?? defaultAccent, theme ?? defaultAppTheme);
            if (theme != null)
                ThemeManager.ChangeAppTheme(Application.Current, theme.Name);
            else
                ThemeManager.ChangeAppTheme(Application.Current, defaultAppTheme.Name);

        }
    }
}
