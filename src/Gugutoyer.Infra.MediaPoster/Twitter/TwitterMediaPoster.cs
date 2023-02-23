using CoreTweet;
using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using Gugutoyer.Application.Interfaces.MediaPoster;
using Gugutoyer.Application.Services.ImageProcessing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Gugutoyer.Infra.MediaPoster.Twitter
{
    public class TwitterMediaPoster : IMediaPoster
    {
        private readonly TwitterMediaPosterSettings _settings;
        private readonly IInputArgsService _inputArgs;
        private readonly IImageProcessor _imageProcessor;
        
        private readonly Tokens token;
        public TwitterMediaPoster(TwitterMediaPosterSettings settings, IInputArgsService inputArgs, IImageProcessor imageProcessor)
        {
            _settings = settings;
            _inputArgs = inputArgs;
            _imageProcessor = imageProcessor;
            token = Tokens.Create(settings.ApiKey, settings.ApiSecret, settings.AccessToken, settings.AccessTokenSecret);
        }
        public async Task<bool> SendStatus(PalavraDTO word)
        {
            var uploadResult = await SendMedia(word);
            
            if (uploadResult is not null && uploadResult.Size > 0)
            {
                try
                {
                    StringBuilder expression = new();

                    if (_inputArgs.HasValidArgs())
                        expression.Append("Edição de Colecionador:\n\n");
                    expression.Append(word.Verbete!.First().ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture) + word.Verbete![1..] + " do Gugu");

                    Console.WriteLine($"Trying to send: {expression}. Media upload result JSON: {uploadResult.Json}\n\nMedia upload id: {uploadResult.MediaId}");

                    await token.Statuses.UpdateAsync(status: expression.ToString(), media_ids: new long[] { uploadResult.MediaId });
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

        private async Task<MediaUploadResult> SendMedia(PalavraDTO word)
        {
            try
            {
                var image = _imageProcessor.CreateImage(word);
                return await token.Media.UploadAsync(media: image.AsEnumerable());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception trying to send media:\n{e.Message}\nStack Trace:\n{e.StackTrace}");
                return null!;
            }
        }

        public async Task<bool> SendWarningMessage(int remaining)
        {
            if (remaining < 100 && (remaining % 5) == 0)
            {
                try
                {
                    var message = $"As mensagens estão começando a ficar escassas, tem tipo {remaining} agora. Faça uma filtragem.";
                    await token.DirectMessages.Events.NewAsync(message, long.Parse(_settings.MessageTargetHandlerId!, CultureInfo.InvariantCulture));
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
    }
}
