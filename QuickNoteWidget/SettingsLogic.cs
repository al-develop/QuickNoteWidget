using System;
using System.IO;
using System.Xml.Serialization;

namespace QuickNoteWidget
{
    public static class SettingsLogic
    {
        private const string CYAN = "Cyan";
        private const string LIGHT = "Light";
        private const string DEFAULT_FONT = "Arial";

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




        public static Settings GetDefaultSettings() => new Settings()
        {
            SelectedAccentName = CYAN,
            SelectedThemeName = LIGHT,
            OnTop = true,
            DisplayDetails = true,
            ShowInTaskbar = true,
            TransparencyValue = 1.0,
            CurrentFont = DEFAULT_FONT,
        };



        private static bool DoesSettingFileExists()
        {
            return File.Exists(SettingsPath);
        }
    }
}