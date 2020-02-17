using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Microsoft.Extensions.FileProviders;

namespace Paramdigma.Core.Utilities
{
    public class ResourcesTests
    {
        [Fact]
        public void ResetSettingsValues_FromResource()
        {
            Console.WriteLine(Settings.Tolerance);
            Console.WriteLine(Settings.MaxDecimals);
            Console.WriteLine(Settings.GetDefaultTesselationLevel());
            Settings.SetTolerance(0.1);
            Console.WriteLine(Settings.Tolerance);
            Console.WriteLine(Settings.MaxDecimals);
            Settings.Reset();
            Console.WriteLine(Settings.Tolerance);
            Console.WriteLine(Settings.MaxDecimals);
        }
    }
}