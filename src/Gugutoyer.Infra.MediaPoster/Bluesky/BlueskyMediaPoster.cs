using FishyFlip;
using FishyFlip.Models;
using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using Gugutoyer.Application.Interfaces.MediaPoster;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.MediaPoster.Bluesky
{
    public class BlueskyMediaPoster : IMediaPoster
    {
        private readonly BlueskyMediaPosterSettings _settings;
        private readonly ATProtocol _protocol;
        private readonly IImageProcessor _imageProcessor;
        private readonly IInputArgsService _inputArgs;

        public BlueskyMediaPoster(BlueskyMediaPosterSettings settings, IImageProcessor imageProcessor, IInputArgsService inputArgs)
        {
            _settings = settings;
            _protocol = new ATProtocolBuilder().Build();
            _imageProcessor = imageProcessor;
            _inputArgs = inputArgs;
        }

        public async Task<bool> SendStatus(PalavraDTO word)
        {
            await CreateSession();
            var uploadResult = await SendMedia(word);
            if (uploadResult is not null && uploadResult.Blob.Size > 0)
            {
                try
                {
                    StringBuilder expression = new();

                    if (_inputArgs.HasValidArgs())
                        expression.Append("Edição de Colecionador:\n\n");
                    expression.Append(word.Verbete!.First().ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture) + word.Verbete![1..] + " do Gugu");

                    Console.WriteLine($"Trying to send: {expression}. \n\nMedia upload id: {uploadResult.Blob.Ref}");
                    await _protocol.Repo.CreatePostAsync(text: expression.ToString(), facets: null, embed: new ImagesEmbed(uploadResult.Blob.ToImage(), expression.ToString()));

                }
                catch (System.Net.WebException we)
                {
                    Console.WriteLine($"WebException caught: {we.Response}\n{we.Message}\n{we.Status}\n{we.Data}\n{we.TargetSite}\n{we.StackTrace}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception trying to send media:\n{e.Message}\nStack Trace:\n{e.StackTrace}");
                    if (e.InnerException is not null)
                        Console.WriteLine($"Inner Exception :\n{e.InnerException.Message}\nStack Trace:\n{e.InnerException.StackTrace}");
                    return false;
                }
            }
            return true;
        }

        private async Task<UploadBlobResponse> SendMedia(PalavraDTO word)
        {
            UploadBlobResponse result = null!;
            try
            {
                var image = _imageProcessor.CreateImage(word);
                using MemoryStream stream = new MemoryStream(image.Length);
                await stream.WriteAsync(image);
                stream.Position = 0;
                var streamContent = new StreamContent(stream);
                streamContent.Headers.ContentLength = stream.Length;
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                var blobResult = await _protocol.Repo.UploadBlobAsync(streamContent);
                blobResult.Switch
                    (
                    success => { result = success; },
                    error => { throw new Exception(error.Detail.Message); }
                    );
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception trying to send media:\n{e.Message}\nStack Trace:\n{e.StackTrace}");
            }
            return result;
        }

        /// <summary>
        /// TODO: How would I be able to get a convoId in the first place...?
        /// </summary>
        /// <param name="remaining"></param>
        /// <returns></returns>
        public async Task<bool> SendWarningMessage(int remaining)
        {
            if (remaining < 100 && (remaining % 5) == 0)
            {
                try
                {
                    var message = $"As mensagens estão começando a ficar escassas, tem tipo {remaining} agora. Faça uma filtragem.";
                    Console.WriteLine(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception trying to send media:\n{e.Message}\nStack Trace:\n{e.StackTrace}");
                    if (e.InnerException is not null)
                        Console.WriteLine($"Inner Exception :\n{e.InnerException.Message}\nStack Trace:\n{e.InnerException.StackTrace}");
                    return false;
                }
            }
            return true;
        }

        private async Task CreateSession()
        {
            Result<Session> result = await _protocol.Server.CreateSessionAsync(_settings.UserName, _settings.AppPassword, CancellationToken.None);

            result.Switch(
                success =>
                {
                    Console.WriteLine($"Bluesky session creation successful");
                },
                error =>
                {
                    Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
                }
            );
        }
    }
}
