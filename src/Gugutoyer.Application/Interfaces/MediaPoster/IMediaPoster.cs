using Gugutoyer.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Interfaces.MediaPoster
{
    public interface IMediaPoster
    {
        public Task<bool> SendStatus(PalavraDTO word);
        public Task<bool> SendWarningMessage(int remaining);
    }
}
