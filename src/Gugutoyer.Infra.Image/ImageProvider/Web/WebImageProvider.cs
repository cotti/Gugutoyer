using GoogleApi.Entities.Search.Common;
using GoogleApi.Entities.Search.Image.Request;
using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.Image.ImageProvider.Web
{
    /// <summary>
    /// TODO: Check DDG support?
    /// </summary>
    public class WebImageProvider : IImageProvider
    {
        private readonly HttpClient _httpClient;
        private readonly WebImageSettings _settings;
        private readonly IInputArgsService _inputArgs;

        private byte[]? rawSourceImg;
        private bool stopTrying = false;

        public WebImageProvider(IHttpClientFactory httpClientFactory, WebImageSettings settings, IInputArgsService inputArgs)
        {
            _httpClient = httpClientFactory.CreateClient("imageprovider");
            _settings = settings;
            _inputArgs = inputArgs;
        }

        public byte[] GetSourceImage(PalavraDTO word)
        {
            int start = _inputArgs.GetIndex();
            if (start == -1)
                start = 0;
            var request = new ImageSearchRequest
            {
                Key = _settings.ApiKey,
                SearchEngineId = _settings.SearchEngineId,
                ImageOptions = new SearchImageOptions()
                {
                    ImageType = GoogleApi.Entities.Search.Common.Enums.ImageType.Photo
                },
                Query = word.Verbete,
                Options = new SearchOptions()
                {
                    StartIndex = start,
                    SafetyLevel = GoogleApi.Entities.Search.Common.Enums.SafetyLevel.High
                }
            };

            var responseEnumerator = GoogleApi.GoogleSearch.ImageSearch.QueryAsync(request).Result.Items.GetEnumerator();

            while (!SafeGetImage(responseEnumerator) && !stopTrying) ;

            return rawSourceImg!;
        }

        private bool SafeGetImage(IEnumerator<Item> items)
        {
            //If the iterator can't continue, we can't proceed
            if (!items.MoveNext())
            {
                stopTrying = true;
                return false;
            }

            string sourceLink = items.Current.Link;
            try
            {
                using Task<byte[]> t = new(() =>
                {
                    try
                    {
                        return _httpClient.GetByteArrayAsync(sourceLink).Result;
                    }
                    catch
                    {
                        return Array.Empty<byte>();
                    }
                });
                TimeSpan ts = TimeSpan.FromSeconds(30);
                t.Start();
                if (!t.Wait(ts) || t.Result is null || t.Result.Length == 0)
                    return false;
                rawSourceImg = t.Result;
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
                return false;
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
                return false;
            }
            return true;
        }
    }
}
