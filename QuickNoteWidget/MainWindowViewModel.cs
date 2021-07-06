using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ControlzEx.Theming;
using DevExpress.Mvvm;

namespace QuickNoteWidget
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _multiLine;
        private string _multiLineTextForegroundColor;
        private string _wordCount;

        public string WordCount
        {
            get { return _wordCount; }
            set { SetProperty(ref _wordCount, value, () => WordCount); }
        }
        public string MultiLineTextForegroundColor
        {
            get { return _multiLineTextForegroundColor; }
            set
            {
                SetProperty(ref _multiLineTextForegroundColor, value, () => MultiLineTextForegroundColor);
            }
        }
        public string MultiLine
        {
            get { return _multiLine; }
            set 
            {
                SetProperty(ref _multiLine, value, () => MultiLine);
            }
        }


        public MainWindowViewModel()
        {
            LoadAvailableThemes();
            LoadSettings();
            Init();
        }

        private void Init()
        {
            ClearMultiLineCommand = new DelegateCommand(ClearMultiLine);
            MultiLine = "";
        }

        private void ClearMultiLine()
        {
            this.MultiLine = "";
        }

        public ICommand ClearMultiLineCommand { get; set; }
        

        #region Settings
        public Settings Settings { get; set; }

        private bool _onTop;
        private ObservableCollection<string> _themes;
        private ObservableCollection<string> _accents;
        private string _selectedTheme;
        private string _selectedAccent;
        private bool _displayDetails;

        public bool DisplayDetails
        {
            get { return _displayDetails; }
            set { SetProperty(ref _displayDetails, value, () => DisplayDetails); }
        }
        public string SelectedAccent
        {
            get { return _selectedAccent; }
            set
            {
                SetProperty(ref _selectedAccent, value, () => SelectedAccent);
                ThemeChanger.ChangeTheme(this.SelectedAccent, this.SelectedTheme);
            }
        }
        public string SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                SetProperty(ref _selectedTheme, value, () => SelectedTheme);
                ThemeChanger.ChangeTheme(this.SelectedAccent, this.SelectedTheme);
                ThemeSelectionChanged();
            }
        }
        public ObservableCollection<string> Accents
        {
            get { return _accents; }
            set { SetProperty(ref _accents, value, () => Accents); }
        }
        public ObservableCollection<string> Themes
        {
            get { return _themes; }
            set { SetProperty(ref _themes, value, () => Themes); }
        }
        public bool OnTop
        {
            get { return _onTop; }
            set { SetProperty(ref _onTop, value, () => OnTop); }
        }


        private void LoadAvailableThemes()
        {
            Themes = new ObservableCollection<string>() { ThemeManager.BaseColorLight, ThemeManager.BaseColorDark };
            Accents = new ObservableCollection<string>(ThemeManager.Current.ColorSchemes);
        }
        private void LoadSettings()
        {
            this.Settings = SettingsLogic.GetSettings();
            OnTop = Settings.OnTop;
            DisplayDetails = Settings.DisplayDetails;
            SelectedTheme = Themes.FirstOrDefault(f => f == this.Settings.SelectedThemeName);
            SelectedAccent = Accents.FirstOrDefault(f => f == this.Settings.SelectedAccentName);
        }


        public void SaveSettings()
        {
            Settings.SelectedAccentName = this.SelectedAccent;
            Settings.SelectedThemeName = this.SelectedTheme;
            Settings.OnTop = this.OnTop;
            Settings.DisplayDetails = this.DisplayDetails;
            SettingsLogic.SaveSettings(this.Settings);
        }

        private void ThemeSelectionChanged()
        {
            if (!String.IsNullOrEmpty(SelectedTheme))
                MultiLineTextForegroundColor = SelectedTheme.ToLower() == "light" ? "black" : "white";
            else
                MultiLineTextForegroundColor = "LightGray";
        }

        #endregion Settings
        
        private bool _isChecked;
        private string _borderBrush;
        private string _foreground;
        private string _tbxForeground;
        private string _tbxFontStyle;
        private string _tbxTextDecorations;

        public string tbxTextDecorations
        {
            get { return _tbxTextDecorations; }
            set { SetProperty(ref _tbxTextDecorations, value, () => tbxTextDecorations); }
        }
        public string tbxFontStyle
        {
            get { return _tbxFontStyle; }
            set { SetProperty(ref _tbxFontStyle, value, () => tbxFontStyle); }
        }
        public string tbxForeground
        {
            get { return _tbxForeground; }
            set { SetProperty(ref _tbxForeground, value, () => tbxForeground); }
        }
        public string Foreground
        {
            get { return _foreground; }
            set { SetProperty(ref _foreground, value, () => Foreground); }
        }
        public string BorderBrush
        {
            get { return _borderBrush; }
            set { SetProperty(ref _borderBrush, value, () => BorderBrush); }
        }
        public bool IsChecked
        {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value, () => IsChecked); }
        }

        public void stpCbxWrapper_MouseDown()
        {
            if (IsChecked != null && IsChecked == true)
            {
                IsChecked = false;
                BorderBrush = "Black";

                tbxForeground = "Black";
                tbxFontStyle = "Normal";
                tbxTextDecorations = null;
            }
            else
            {
                IsChecked = true;
                BorderBrush = "Gray";
                Foreground = "Gray";

                tbxForeground = "Gray";
                tbxFontStyle = "Italic";
                tbxTextDecorations = "Strikethrough";
            }
        }
    }
}