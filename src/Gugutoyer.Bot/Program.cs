using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.Data;
using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using Gugutoyer.Application.Interfaces.MediaPoster;
using Gugutoyer.Application.Services;
using Gugutoyer.Domain.Entities;
using Gugutoyer.Infra.CrossCutting.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            var inputArgs = services.GetServices<IInputArgsService>();
            var mediaPoster = services.GetRequiredService<IMediaPoster>();

            bool specialEdition = await RunSpecialEdition(inputArgs, mediaPoster);

            if (!specialEdition)
            {
                var filtros = services.GetRequiredService<IFiltrosService>();
                var palavras = services.GetRequiredService<IPalavrasService>();

                var remaining = await filtros.CountRemaining();

                if (remaining <= 100)
                    await mediaPoster.SendWarningMessage(remaining);

                if (remaining > 0)
                    await RunDictionary(filtros, palavras, mediaPoster);
            }
        }

        public static async Task<bool> RunSpecialEdition(IEnumerable<IInputArgsService> inputArgs, IMediaPoster mediaPoster)
        {
            foreach (var arg in inputArgs)
            {
                if (arg.HasValidArgs())
                {
                    await mediaPoster.SendStatus(new PalavraDTO() { Verbete = arg.GetWord() });
                    return true;
                }
            }
            return false;
        }

        public static async Task RunDictionary(IFiltrosService filtros, IPalavrasService palavras, IMediaPoster mediaPoster)
        {
            var nextWord = await filtros.GetNext();
            await palavras.UpdateUsed(new List<PalavraDTO>() { nextWord.Palavra! });
            await mediaPoster.SendStatus(nextWord.Palavra!);
            await filtros.Delete(new List<FiltroDTO>() { nextWord });
        }
    }
}