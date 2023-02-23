using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Interfaces.Environment
{
    public interface IInputArgsService
    {
        bool HasValidArgs();
        int GetIndex();
        string GetWord();
    }
}
