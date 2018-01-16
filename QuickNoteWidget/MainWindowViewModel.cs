using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using MahApps.Metro;

namespace QuickNoteWidget
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<CheckItemTemplate> _checkItems;
        private string _singleLine;
        private string _multiLine;
        private string _addEntry;
        private string _multiLineTextForegroundColor;

        public string MultiLineTextForegroundColor
        {
            get { return _multiLineTextForegroundColor; }
            set
            {
                SetProperty(ref _multiLineTextForegroundColor, value, () => MultiLineTextForegroundColor);
            }
        }
        public string AddEntry
        {
            get { return _addEntry; }
            set { SetProperty(ref _addEntry, value, () => AddEntry); }
        }
        public string MultiLine
        {
            get { return _multiLine; }
            set { SetProperty(ref _multiLine, value, () => MultiLine); }
        }
        public string SingleLine
        {
            get { return _singleLine; }
            set { SetProperty(ref _singleLine, value, () => SingleLine); }
        }
        public ObservableCollection<CheckItemTemplate> CheckItems
        {
            get { return _checkItems; }
            set { SetProperty(ref _checkItems, value, () => CheckItems); }
        }

        public MainWindowViewModel()
        {
            LoadSettings();
            LoadAvailableThemes();
            Init();

        }

        private void Init()
        {
            AddToListCommand = new DelegateCommand(AddToList);
            ClearCheckListCommand = new DelegateCommand(ClearCheckList);
            SingleLine = "";
            MultiLine = "";
            AddEntry = "";
            CheckItems = new ObservableCollection<CheckItemTemplate>();
            OnTop = Settings.OnTop;

        }

        private void ClearCheckList()
        {
            this.CheckItems.Clear();
        }

        private void AddToList()
        {
            CheckItemTemplate newEntry = new CheckItemTemplate();
            newEntry.IsChecked = false;
            newEntry.Description = AddEntry;
            CheckItems.Add(newEntry);

            this.AddEntry = null;
        }

        public ICommand AddToListCommand { get; set; }
        public ICommand ClearCheckListCommand { get; set; }

        #region Settings
        public Settings Settings { get; set; }

        private bool _onTop;
        private ObservableCollection<AppTheme> _themes;
        private ObservableCollection<Accent> _accents;
        private AppTheme _selectedTheme;
        private Accent _selectedAccent;

        public Accent SelectedAccent
        {
            get { return _selectedAccent; }
            set
            {
                SetProperty(ref _selectedAccent, value, () => SelectedAccent);
                ThemeChanger.ChangeTheme(this.SelectedAccent, this.SelectedTheme);
            }
        }
        public AppTheme SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                SetProperty(ref _selectedTheme, value, () => SelectedTheme);
                ThemeChanger.ChangeTheme(this.SelectedAccent, this.SelectedTheme);
                ThemeSelectionChanged();
            }
        }

        private void ThemeSelectionChanged()
        {
            if (SelectedTheme.Name.ToLower() == "baselight")
                MultiLineTextForegroundColor = "black";
            else
                MultiLineTextForegroundColor = "white";
        }

        public ObservableCollection<Accent> Accents
        {
            get { return _accents; }
            set { SetProperty(ref _accents, value, () => Accents); }
        }
        public ObservableCollection<AppTheme> Themes
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
            Themes = new ObservableCollection<AppTheme>(ThemeManager.AppThemes);
            Accents = new ObservableCollection<Accent>(ThemeManager.Accents);
            SelectedTheme = ThemeManager.GetAppTheme(this.Settings.SelectedThemeName);
            SelectedAccent = ThemeManager.GetAccent(this.Settings.SelectedAccentName);
        }
        private void LoadSettings()
        {
            this.Settings = SettingsLogic.GetSettings();
        }


        public void SaveSettings()
        {
            Settings.SelectedAccentName = this.SelectedAccent.Name;
            Settings.SelectedThemeName = this.SelectedTheme.Name;
            Settings.OnTop = this.OnTop;
            SettingsLogic.SaveSettings(this.Settings);
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