using System;
using System.IO;
using System.Xml.Serialization;
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

        private string _currentFont;

        public string CurrentFont
        {
            get { return _currentFont; }
            set { SetProperty(ref _currentFont, value, () => CurrentFont); }
        }
    }

    public static class SettingsLogic
    {
        private const string CYAN = "Cyan";
        private const string LIGHT = "Light";
        private static readonly string SettingsPath = $"{AppDomain.CurrentDomain.BaseDirectory}settings.xml";

        public static void SaveSettings(Settings settings)
        {
            if (DoesSettingFileExists())
                File.Delete(SettingsPath);

            SerializeToFile(settings);
        }


        public static Settings GetSettings()
        {
            if (DoesSettingFileExists() == false)
                return SettingsLogic.GetDefaultSettings();

            return DeserializeFromFile();
        }



        private static void SerializeToFile(Settings settings)
        {
            using (FileStream stream = new FileStream(SettingsPath, FileMode.OpenOrCreate))
            {
                var serializer = new XmlSerializer(typeof(Settings));
                serializer.Serialize(stream, settings);
            }
        }

        private static Settings DeserializeFromFile()
        {
            using (FileStream stream = new FileStream(SettingsPath, FileMode.OpenOrCreate))
            {
                var serializer = new XmlSerializer(typeof(Settings));
                return serializer.Deserialize(stream) as Settings;
            }
        }




        private static Settings GetDefaultSettings() => new Settings()
        {
            SelectedAccentName = CYAN,
            SelectedThemeName = LIGHT,
            OnTop = false,
            DisplayDetails = false,
            ShowInTaskbar = false,
            TransparencyValue = 0.3,
        };



        private static bool DoesSettingFileExists()
        {
            return File.Exists(SettingsPath);
        }
    }
}