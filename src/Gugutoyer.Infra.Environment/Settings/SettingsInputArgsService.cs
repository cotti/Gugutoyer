using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Infra.Environment.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.Environment.Environment
{
    public class SettingsInputArgsService : IInputArgsService
    {
        private readonly SettingsInputSettings _settings;
        public SettingsInputArgsService(SettingsInputSettings settings)
        {
            _settings = settings;
        }

        public int GetIndex() => _settings.Index;

        public string GetWord() => _settings.Word ?? "";

        public bool HasValidArgs() => _settings.Index > -1 && !string.IsNullOrWhiteSpace(_settings.Word);
    }
}
