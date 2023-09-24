using CommunityToolkit.Mvvm.ComponentModel;
using CoreTweet;
using DynamicData.Kernel;
using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading.Tasks;

namespace Gugutoyer.Manager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IMultipleImageProvider? imageProvider;

        [ObservableProperty]
        private readonly ConcurrentBag<ScrapedImageDTO> scrapedImages;

        public MainWindowViewModel(IMultipleImageProvider? imageProvider = null) : this()
        {
            this.scrapedImages = new();
            this.imageProvider = imageProvider;
        }
        public MainWindowViewModel() { }

        private async Task GetImages()
        {
            await foreach (var image in imageProvider!.ScrapImages(new Application.DTOs.PalavraDTO() { Verbete = "gugu" }))
            {
                scrapedImages.Add(image);
            }
        }

        public string Greeting => "Welcome to Avalonia!";
    }
}