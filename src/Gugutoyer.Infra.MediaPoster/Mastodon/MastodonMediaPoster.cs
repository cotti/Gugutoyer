﻿using CoreTweet;
using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using Gugutoyer.Application.Interfaces.MediaPoster;
using Gugutoyer.Infra.MediaPoster.Twitter;
using Mastonet;
using Mastonet.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.MediaPoster.Mastodon
{
    public class MastodonMediaPoster : IMediaPoster
    {
        private readonly MastodonMediaPosterSettings _settings;
        private readonly IInputArgsService _inputArgs;
        private readonly IImageProcessor _imageProcessor;
        private readonly MastodonClient _client;
        public MastodonMediaPoster(MastodonMediaPosterSettings settings, IInputArgsService inputArgs, IImageProcessor imageProcessor, IHttpClientFactory httpClientFactory)
        {
            _settings = settings;
            _inputArgs = inputArgs;
            _imageProcessor = imageProcessor;
            _client = new MastodonClient(_settings.Instance!, _settings.AccessToken!, httpClientFactory.CreateClient("mastodon"));
        }
        public async Task<bool> SendStatus(PalavraDTO word)
        {
            var uploadResult = await SendMedia(word);

            if (uploadResult is not null && uploadResult.Id.Length > 0)
            {
                try
                {
                    StringBuilder expression = new();

                    if (_inputArgs.HasValidArgs())
                        expression.Append("Edição de Colecionador:\n\n");
                    expression.Append(word.Verbete!.First().ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture) + word.Verbete![1..] + " do Gugu");

                    Console.WriteLine($"Trying to send: {expression}. Media upload result: {uploadResult.Description}\n\nMedia upload id: {uploadResult.Id}");

                    await _client.PublishStatus(sensitive: true, spoilerText: "CW: Input aleatório",  status: expression.ToString(), mediaIds: new string[] { uploadResult.Id });
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

        private async Task<Attachment> SendMedia(PalavraDTO word)
        {
            try
            {
                using var image = new MemoryStream(_imageProcessor.CreateImage(word));
                
                return await _client.UploadMedia(new MediaDefinition(image,word.Verbete!));
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
                    var message = $"{_settings.MessageTargetHandlerId} | As mensagens estão começando a ficar escassas, tem tipo {remaining} agora. Faça uma filtragem.";
                    await _client.PublishStatus(sensitive: false, status: message, visibility: Visibility.Private);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception trying to send message:\n{e.Message}\nStack Trace:\n{e.StackTrace}");
                    if (e.InnerException is not null)
                        Console.WriteLine($"Inner Exception :\n{e.InnerException.Message}\nStack Trace:\n{e.InnerException.StackTrace}");
                    return false;
                }
            }
            return true;
        }
    }
}
