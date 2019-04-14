using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace AR_Lib
{
    /// <summary>
    /// Multi-layered struct that holds the library settings
    /// </summary>
    public class Settings
    {
        public static double Tolerance => _tolerance;
        public static double DefaultTesselation = 10;
        public static int MaxDecimals => _maxDecimals;

        private static int _maxDecimals = 8;
        private static double _tolerance = 0.0000001;
        public static void ModifyTolerance(double tolerance)
        {
            _tolerance = tolerance;
            string t = tolerance.ToString("N14");
            _maxDecimals = t.Substring(t.IndexOf(".") + 1).IndexOf("1");
        }

    }

    public static class SettingsReader
    {

        public static Settings ReadSettings()
        {
            var fileName = "Settings.json";
            var assembly = Assembly.GetExecutingAssembly();

            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(fileName));
            string settingsString;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))

            {
                settingsString = reader.ReadToEnd();

            }
            Settings deserializedSettings = JsonConvert.DeserializeObject<Settings>(settingsString);

            return deserializedSettings;
        }
    }
}