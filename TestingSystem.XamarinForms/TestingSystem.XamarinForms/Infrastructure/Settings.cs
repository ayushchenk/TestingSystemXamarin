using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestingSystem.XamarinForms.Infrastructure
{
    public static class Settings
    {
        private const string settingsFilePath = "ies_settings.txt";

        public static bool UseListLayout { set; get; }

        public static void WriteSettings()
        {
            File.WriteAllText(settingsFilePath, UseListLayout.ToString(), Encoding.Default);
        }

        public static void ReadSettings()
        {
            if (!File.Exists(settingsFilePath))
                return;
            string useListLayout = File.ReadAllText(settingsFilePath, Encoding.Default);
            UseListLayout = bool.Parse(useListLayout);
        }
    }
}
