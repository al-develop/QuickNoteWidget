using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Text;
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
        private const string GRAY = "#616161";  // slightly lighter than BLACK
        private const string LIGHT_GRAY = "#EAEAEA"; // slightly darker than "WhiteSmoke"
        #endregion Constants

        #region MVVM Properties
        private string _multiLine;
        private string _multiLineTextForegroundColor;
        private string _wordCount;
        private string _dragAreaColor;
        private string _statusBarBackground;
        private double _transparencyValue;
        private int _transparencyInPercent;
        private ObservableCollection<string> _fonts;
        private ObservableCollection<string> _themes;
        private ObservableCollection<string> _accents;
        private string _selectedTheme;
        private string _selectedAccent;
        private bool _ontop;
        private bool _showintaskbar;
        private bool _displaydetails;
        private string _currentfont;
        private int _windowHeight;
        private int _windowWidht;
        private bool _isWindowResizeBarVisible;

        public bool IsWindowResizeBarVisible
        {
            get { return _isWindowResizeBarVisible; }
            set { SetProperty(ref _isWindowResizeBarVisible, value, () => IsWindowResizeBarVisible); }
        }
        public int WindowWidht
        {
            get { return _windowWidht; }
            set { SetProperty(ref _windowWidht, value, () => WindowWidht); }
        }
        public int WindowHeight
        {
            get { return _windowHeight; }
            set { SetProperty(ref _windowHeight, value, () => WindowHeight); }
        }
        public string CurrentFont
        {
            get { return _currentfont; }
            set { SetProperty(ref _currentfont, value, () => CurrentFont); }
        }
        public bool OnTop
        {
            get { return _ontop; }
            set { SetProperty(ref _ontop, value, () => OnTop); }
        }
        public bool DisplayDetails
        {
            get { return _displaydetails; }
            set { SetProperty(ref _displaydetails, value, () => DisplayDetails); }
        }
        public bool ShowInTaskbar
        {
            get { return _showintaskbar; }
            set { SetProperty(ref _showintaskbar, value, () => ShowInTaskbar); }
        }
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
                DragAreaColor = this.SelectedTheme == ThemeManager.BaseColorLight ? LIGHT_GRAY : GRAY;
                ThemeChanger.ChangeTheme(this.SelectedAccent, this.SelectedTheme);
                UpdateFontColorOnThemeSelectionChanged();
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
        public ObservableCollection<string> Fonts
        {
            get { return _fonts; }
            set { SetProperty(ref _fonts, value, () => Fonts); }
        }
        public int TransparencyInPercent
        {
            get { return _transparencyInPercent; }
            set { SetProperty(ref _transparencyInPercent, value, () => TransparencyInPercent); }
        }
        public double TransparencyValue
        {
            get { return _transparencyValue; }
            set
            {
                SetProperty(ref _transparencyValue, value, () => TransparencyValue);
                TransparencyInPercent = (Int32)(TransparencyValue * 100);
            }
        }
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

        #endregion MVVM Properties


        public Settings Settings { get; set; }
        
        public ICommand ResetViewCommand { get; set; }
        public ICommand ReduceWindowSizeCommand { get; set; }
        public ICommand IncreaseWindowSizeCommand { get; set; }



        public MainWindowViewModel()
        {
            LoadAvailableThemesAndAccents();
            LoadSettings(SettingsLoadLocations.FromFile);
            InitViewModel();
        }


        private void LoadAvailableThemesAndAccents()
        {
            Themes = new ObservableCollection<string>() { ThemeManager.BaseColorLight, ThemeManager.BaseColorDark };
            Accents = new ObservableCollection<string>(ThemeManager.Current.ColorSchemes);
        }

        private void LoadSettings(SettingsLoadLocations location)
        {
            InitSettingsBeforePropertiesUpdate(location);
            UpdateProperties();
        }

        private void InitSettingsBeforePropertiesUpdate(SettingsLoadLocations location)
        {
            switch (location)
            {
                case SettingsLoadLocations.FromFile:
                    Settings = SettingsLogic.GetSettings();
                    break;
                case SettingsLoadLocations.Default:
                    Settings = SettingsLogic.GetDefaultSettings();
                    break;
            }
        }

        private void UpdateProperties()
        {
            this.SelectedTheme = Themes.FirstOrDefault(f => f == this.Settings.SelectedThemeName);
            this.SelectedAccent = Accents.FirstOrDefault(f => f == this.Settings.SelectedAccentName);
            this.TransparencyValue = Settings.TransparencyValue;
            this.OnTop = Settings.OnTop;
            this.CurrentFont = Settings.CurrentFont;
            this.DisplayDetails = Settings.DisplayDetails;
            this.ShowInTaskbar = Settings.ShowInTaskbar;
        }

        private void InitViewModel()
        {
            WindowHeight = 350;
            WindowWidht = 650;
            IsWindowResizeBarVisible = true;
            Fonts = new ObservableCollection<string>(LoadInstalledFonts());
            ResetViewCommand = new DelegateCommand(ResetView);
            ReduceWindowSizeCommand = new DelegateCommand(ReduceWindowSize);
            IncreaseWindowSizeCommand = new DelegateCommand(IncreaseWindowSize);
        }

        private void ReduceWindowSize()
        {
            if(this.WindowHeight > 100)
                this.WindowHeight -= 15;
            if(IsWindowResizeBarVisible)
                if(this.WindowWidht > 500)
                    this.WindowWidht -= 15;
        }

        private void IncreaseWindowSize()
        {
            this.WindowHeight += 15;
            this.WindowWidht += 15;
        }

        private IEnumerable<string> LoadInstalledFonts()
        {
            using (var fonts = new InstalledFontCollection())
                foreach (FontFamily font in fonts.Families)
                    yield return font.Name;
        }

        private void ResetView()
        {
            Settings defaultSettings = SettingsLogic.GetDefaultSettings();
            LoadSettings(SettingsLoadLocations.Default);
        }

        private void UpdateFontColorOnThemeSelectionChanged()
        {
            if (!String.IsNullOrEmpty(SelectedTheme))
                MultiLineTextForegroundColor = SelectedTheme == ThemeManager.BaseColorLight ? BLACK : WHITE;
            else
                MultiLineTextForegroundColor = LIGHT_GRAY;
        }

        public void SaveSettingsOnClose()
        {
            Settings.SelectedAccentName = this.SelectedAccent;
            Settings.SelectedThemeName = this.SelectedTheme;
            Settings.TransparencyValue = this.TransparencyValue;
            Settings.OnTop = this.OnTop;
            Settings.CurrentFont = this.CurrentFont;
            Settings.DisplayDetails = this.DisplayDetails;
            Settings.ShowInTaskbar = this.ShowInTaskbar;
            SettingsLogic.SaveSettings(this.Settings);
        }
    }
}