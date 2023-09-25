using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.Data;
using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using Gugutoyer.Application.Interfaces.MediaPoster;
using Gugutoyer.Application.Services;
using Gugutoyer.Domain.Entities;
using Gugutoyer.Infra.CrossCutting.DependencyInjection;
using Gugutoyer.Infra.MediaPoster.Mastodon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Gugutoyer.Bot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDatabase(context.Configuration)
                    .AddMapping(context.Configuration)
                    .AddMediator(context.Configuration)
                    .AddCommandLineArgs(context.Configuration)
                    .AddImageProcessing(context.Configuration)
                    .AddMediaPoster(context.Configuration);
                }).Build();

            host.Start();

            await RunBot(host.Services);
        }

        public static async Task RunBot(IServiceProvider services)
        {
            if (services.GetService<MastodonMediaPosterRegistrationHelper>() is not null)
            {
                var helper = services.GetRequiredService<MastodonMediaPosterRegistrationHelper>();
                helper.Register();
            }
            var inputArgs = services.GetServices<IInputArgsService>();
            var mediaPosters = services.GetServices<IMediaPoster>();

            if (mediaPosters.Any())
            {
                Console.WriteLine("No media posters available. Shutting down.");
                return;
            }

            bool specialEdition = await RunSpecialEdition(inputArgs, mediaPosters);

            if (!specialEdition)
            {
                var filtros = services.GetRequiredService<IFiltrosService>();
                var palavras = services.GetRequiredService<IPalavrasService>();

                var remaining = await filtros.CountRemaining();

                if (remaining <= 100)
                    foreach(var poster in mediaPosters)
                        await poster.SendWarningMessage(remaining);

                if (remaining > 0)
                    await RunDictionary(filtros, palavras, mediaPosters);
            }
        }

        public static async Task<bool> RunSpecialEdition(IEnumerable<IInputArgsService> inputArgs, IEnumerable<IMediaPoster> mediaPosters)
        {
            foreach (var arg in inputArgs)
            {
                if (arg.HasValidArgs())
                {
                    foreach (var poster in mediaPosters)
                        await poster.SendStatus(new PalavraDTO() { Verbete = arg.GetWord() });
                    return true;
                }
            }
            return false;
        }

        public static async Task RunDictionary(IFiltrosService filtros, IPalavrasService palavras, IEnumerable<IMediaPoster> mediaPosters)
        {
            var nextWord = await filtros.GetNext();
            await palavras.UpdateUsed(new List<PalavraDTO>() { nextWord.Palavra! });
            foreach (var poster in mediaPosters)
                await poster.SendStatus(nextWord.Palavra!);
            await filtros.Delete(new List<FiltroDTO>() { nextWord });
        }
    }
}