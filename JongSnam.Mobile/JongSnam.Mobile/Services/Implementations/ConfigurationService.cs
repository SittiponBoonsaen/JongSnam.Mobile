using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace JongSnam.Mobile.Services.Implementations
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly string configurationFile = "appsettings.json";

        public Configuration Configuration { get; private set; }

        public ConfigurationService()
        {
            var embeddedResourceStream = Assembly.GetAssembly(typeof(Configuration)).GetManifestResourceStream(configurationFile);
            if (embeddedResourceStream == null)
                return;

            using (var streamReader = new StreamReader(embeddedResourceStream))
            {
                var jsonString = streamReader.ReadToEnd();
                Configuration = JsonConvert.DeserializeObject<Configuration>(jsonString);
            }
        }
    }
}
