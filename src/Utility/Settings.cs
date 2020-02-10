using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AR_Lib
{
    /// <summary>
    /// Multi-layered struct that holds the library settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Gets the minimum value allowed when using this library.
        /// </summary>
        public static double Tolerance { get; private set; } = 0.0000001;

        /// <summary>
        /// Gets how many decimals are allowed when using the library.
        /// </summary>
        public static int MaxDecimals { get; private set; } = 8;

        /// <summary>
        /// Modifies the tolerance and computes the maxDecimals value accordingly.
        /// </summary>
        /// <param name="tolerance">Desired tolerance.</param>
        public static void ModifyTolerance(double tolerance)
        {
            Tolerance = tolerance;
            string t = tolerance.ToString("N14");
            MaxDecimals = t.Substring(t.IndexOf(".") + 1).IndexOf("1");
        }
    }
}