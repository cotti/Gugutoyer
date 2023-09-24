using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.Image.MultipleImageProvider.Mock
{
    internal class MockMultipleImageProvider : IMultipleImageProvider
    {
        public async IAsyncEnumerable<ScrapedImageDTO> ScrapImages(PalavraDTO word)
        {
            yield return await Task.FromResult(new ScrapedImageDTO());
        }
    }
}
