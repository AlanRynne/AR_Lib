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
    public struct Settings
    {
        /// <summary>
        /// Constants of the library
        /// </summary>
        public ConstStruct Constants;


        /// <summary>
        /// Data structure that save the library constants
        /// </summary>
        public struct ConstStruct
        {
            public double Tolerance;
            public double DefaultTesselation;
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
