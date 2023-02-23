using Gugutoyer.Application.Interfaces.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.Environment.Environment
{
    public class CommandLineInputArgsService : IInputArgsService
    {
        private readonly IList<string> _commandLineArgs;
        private int _searchIndex = -1;
        private string? _specialWord;
        public CommandLineInputArgsService()
        {
            _commandLineArgs = System.Environment.GetCommandLineArgs().AsReadOnly();
            ParseCommandLine();
        }

        private void ParseCommandLine()
        {
            int searchIndexOffset = _commandLineArgs.IndexOf("-i") + 1;
            if (searchIndexOffset != 0)
                _searchIndex = int.Parse(_commandLineArgs[searchIndexOffset]);
            else
            {
                searchIndexOffset = _commandLineArgs.IndexOf("--index") + 1;
                if (searchIndexOffset != 0)
                    _searchIndex = int.Parse(_commandLineArgs[searchIndexOffset]);
            }

            int specialWordOffset = _commandLineArgs.IndexOf("-s") + 1;
            if (specialWordOffset != 0)
                _specialWord = _commandLineArgs[specialWordOffset];
            else
            {
                specialWordOffset = _commandLineArgs.IndexOf("--special") + 1;
                if (specialWordOffset != 0)
                    _specialWord = _commandLineArgs[specialWordOffset];
            }
        }
        public int GetIndex() => _searchIndex;

        public string GetWord() => _specialWord ?? "";

        public bool HasValidArgs() => _searchIndex > -1 && !string.IsNullOrWhiteSpace(_specialWord);
    }
}
