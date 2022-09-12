using DevExpress.Mvvm;

namespace QuickNoteWidget
{
    public class Settings : BindableBase
    {
        public string SelectedThemeName { get; set; }
        public string SelectedAccentName { get; set; }
        public bool OnTop { get; set; }
        public bool DisplayDetails { get; set; }
        public bool ShowInTaskbar { get; set; }
        public double TransparencyValue { get; set; }
        public string CurrentFont { get; set; }
    }
}