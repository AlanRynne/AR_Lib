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
        private static int _maxDecimals = 8;
        private static double _tolerance = 0.0000001;

        /// <summary>
        /// Determines the minimum value allowed when using this library.
        /// </summary>
        public static double Tolerance => _tolerance;

        /// <summary>
        /// Determines how many decimals are allowed when using the library.
        /// </summary>
        public static int MaxDecimals => _maxDecimals;

        /// <summary>
        /// Modifies the tolerance and computes the maxDecimals value accordingly.
        /// </summary>
        /// <param name="tolerance">Desired tolerance</param>
        public static void ModifyTolerance(double tolerance)
        {
            _tolerance = tolerance;
            string t = tolerance.ToString("N14");
            _maxDecimals = t.Substring(t.IndexOf(".") + 1).IndexOf("1");
        }

    }
}