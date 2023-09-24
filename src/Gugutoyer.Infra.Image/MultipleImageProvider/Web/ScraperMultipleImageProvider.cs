using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GScraper;
using System.Net;

namespace Gugutoyer.Infra.Image.MultipleImageProvider.Web
{
    public class ScraperMultipleImageProvider : IMultipleImageProvider
    {
        private readonly HttpClient _httpClient;

        public ScraperMultipleImageProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("imagescraper");
        }
        public async IAsyncEnumerable<ScrapedImageDTO> ScrapImages(PalavraDTO word)
        {
            var scraper = new GScraper.DuckDuckGo.DuckDuckGoScraper(_httpClient);
            var scrapedUrls = await scraper.GetImagesAsync(word.Verbete!,
                SafeSearchLevel.Moderate,
                GScraper.DuckDuckGo.DuckDuckGoImageTime.Any,
                GScraper.DuckDuckGo.DuckDuckGoImageSize.All,
                GScraper.DuckDuckGo.DuckDuckGoImageColor.All,
                GScraper.DuckDuckGo.DuckDuckGoImageType.All, GScraper.DuckDuckGo.DuckDuckGoImageLayout.All, GScraper.DuckDuckGo.DuckDuckGoImageLicense.All, "pt-br");

            foreach(var scrapedUrl in scrapedUrls)
            {
                yield return new ScrapedImageDTO() { URL = scrapedUrl.Url, Image = SafeGetImage(scrapedUrl.Url) };
            }
        }

        private byte[] SafeGetImage(string itemUrl)
        {
            try
            {
                using Task<byte[]> t = new(() =>
                {
                    try
                    {
                        return _httpClient.GetByteArrayAsync(itemUrl).Result;
                    }
                    catch
                    {
                        return Array.Empty<byte>();
                    }
                });
                TimeSpan ts = TimeSpan.FromSeconds(30);
                t.Start();
                if (!t.Wait(ts) || t.Result is null || t.Result.Length == 0)
                    return Array.Empty<byte>();
                return t.Result;
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
                return Array.Empty<byte>();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                return Array.Empty<byte>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
                return Array.Empty<byte>();
            }
        }
    }
}
