using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.Environment.Settings
{
    public class SettingsInputSettings
    {
        public string Word { get; set; }
        public int Index { get; set; }
        public SettingsInputSettings(IConfiguration configuration)
        {
            Index = int.Parse(configuration.GetSection("SettingsInput")["Index"]!);
            Word = configuration.GetSection("SettingsInput")["Word"]!;
        }
    }
}
