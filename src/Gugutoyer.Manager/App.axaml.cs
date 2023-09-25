using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Gugutoyer.Manager.ViewModels;
using Gugutoyer.Manager.Views;
using Gugutoyer.Infra.CrossCutting.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Gugutoyer.Application.Interfaces.ImageProcessing;

namespace Gugutoyer.Manager
{
    public partial class App : Avalonia.Application
    {
        private static IHost? ServiceHost;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            ServiceHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDatabase(context.Configuration)
                    .AddMapping(context.Configuration)
                    .AddMediator(context.Configuration)
                    .AddImageProcessing(context.Configuration)
                    .AddMediaPoster(context.Configuration);
                }).Build();
            ServiceHost.Start();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(ServiceHost?.Services.GetRequiredService<IMultipleImageProvider>()),
                };

            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}