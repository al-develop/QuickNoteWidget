using DevExpress.Mvvm;

namespace QuickNoteWidget
{
    public class Settings : BindableBase
    {
        public string SelectedThemeName { get; set; } = "Light";
        public string SelectedAccentName { get; set; } = "Cyan";
        public bool OnTop { get; set; } = true;
        public bool DisplayDetails { get; set; } = true;
        public bool ShowInTaskbar { get; set; } = true;
        public double TransparencyValue { get; set; } = 1;
        public string CurrentFont { get; set; } = "Arial";
    }
}