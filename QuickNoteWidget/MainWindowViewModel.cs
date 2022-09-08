﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ControlzEx.Theming;
using DevExpress.Mvvm;
using QuickNoteWidget.Theme;

namespace QuickNoteWidget
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constants
        private const string WHITE = "#FFFFFF";
        private const string BLACK = "#252525"; // Based on Theme colord "Dark"
        private const string GRAY = "#3A3A3A";  // slightly lighter than BLACK
        private const string WHITE_SMOKE = "#EAEAEA"; // slightly darker than "WhiteSmoke"
        private const string NORMAL = "Normal";
        private const string ITALIC = "Italic";
        private const string STRIKETHROUGH = "Strikethrough";
        #endregion

        private string _multiLine;
        private string _multiLineTextForegroundColor;
        private string _wordCount;
        private string _dragAreaColor;
        private string _statusBarBackground;

        public string StatusBarBackground
        {
            get => _statusBarBackground;
            set => SetProperty(ref _statusBarBackground, value, () => StatusBarBackground);
        }
        public string DragAreaColor
        {
            get => _dragAreaColor;
            set => SetProperty(ref _dragAreaColor, value, () => DragAreaColor);
        }
        public string WordCount
        {
            get => _wordCount;
            set => SetProperty(ref _wordCount, value, () => WordCount);
        }
        public string MultiLineTextForegroundColor
        {
            get => _multiLineTextForegroundColor;
            set => SetProperty(ref _multiLineTextForegroundColor, value, () => MultiLineTextForegroundColor);
        }
        public string MultiLine
        {
            get => _multiLine;
            set => SetProperty(ref _multiLine, value, () => MultiLine);
        }


        public ICommand ClearMultiLineCommand { get; set; }

        public MainWindowViewModel()
        {
            LoadAvailableThemes();
            LoadSettings();
            Init();
        }

        private void Init()
        {
            ClearMultiLineCommand = new DelegateCommand(ClearMultiLine);
            ClearMultiLine();
        }

        private void ClearMultiLine() => this.MultiLine = String.Empty;



        #region Settings
        //public Settings Settings { get; set; }

        private Settings _settings;

        public Settings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value, () => Settings); }
        }


        private ObservableCollection<string> _themes;
        private ObservableCollection<string> _accents;
        private string _selectedTheme;
        private string _selectedAccent;
        public string SelectedAccent
        {
            get => _selectedAccent;
            set
            {
                SetProperty(ref _selectedAccent, value, () => SelectedAccent);
                ThemeChanger.ChangeTheme(this.SelectedAccent, this.SelectedTheme);
            }
        }
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                SetProperty(ref _selectedTheme, value, () => SelectedTheme);
                StatusBarBackground = this.SelectedTheme == ThemeManager.BaseColorLight ? WHITE : BLACK;
                DragAreaColor = this.SelectedTheme == ThemeManager.BaseColorLight ? WHITE_SMOKE : GRAY;
                ThemeChanger.ChangeTheme(this.SelectedAccent, this.SelectedTheme);
                ThemeSelectionChanged();
            }
        }
        public ObservableCollection<string> Accents
        {
            get => _accents;
            set => SetProperty(ref _accents, value, () => Accents);
        }
        public ObservableCollection<string> Themes
        {
            get => _themes;
            set => SetProperty(ref _themes, value, () => Themes);
        }


        private void LoadAvailableThemes()
        {
            Themes = new ObservableCollection<string>() { ThemeManager.BaseColorLight, ThemeManager.BaseColorDark };
            Accents = new ObservableCollection<string>(ThemeManager.Current.ColorSchemes);
        }

        private void LoadSettings()
        {
            this.Settings = SettingsLogic.GetSettings();
            SelectedTheme = Themes.FirstOrDefault(f => f == this.Settings.SelectedThemeName);
            SelectedAccent = Accents.FirstOrDefault(f => f == this.Settings.SelectedAccentName);
        }


        public void SaveSettings()
        {
            Settings.SelectedAccentName = this.SelectedAccent;
            Settings.SelectedThemeName = this.SelectedTheme;
            SettingsLogic.SaveSettings(this.Settings);
        }

        private void ThemeSelectionChanged()
        {
            if (!String.IsNullOrEmpty(SelectedTheme))
                MultiLineTextForegroundColor = SelectedTheme == ThemeManager.BaseColorLight ? BLACK : WHITE;
            else
                MultiLineTextForegroundColor = WHITE_SMOKE;
        }

        #endregion Settings
    }
}