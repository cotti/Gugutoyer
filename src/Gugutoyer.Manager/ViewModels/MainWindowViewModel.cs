using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoreTweet;
using DynamicData.Kernel;
using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using Gugutoyer.Manager.Models;
using System;
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
        private ObservableCollection<ScrapedImageModel> scrapedImages = new();

        public MainWindowViewModel() : this(null!) { }
        public MainWindowViewModel(IMultipleImageProvider? imageProvider)
        {
            this.imageProvider = imageProvider;
            if(Design.IsDesignMode)
            {
                ScrapedImages.Add(new(new byte[] {}, "asd"));
            }
        }

        [RelayCommand]
        private async Task GetImagesAsync()
        {
            _ = Task.Run(async () =>
            {
                await foreach (var image in imageProvider!.ScrapImages(new Application.DTOs.PalavraDTO() { Verbete = "gugu" }))
                {
                    ScrapedImages.Add(new(image.Image ?? Array.Empty<byte>(), image.URL ?? ""));
                }
            });
            //await foreach (var image in imageProvider!.ScrapImages(new Application.DTOs.PalavraDTO() { Verbete = "gugu" }))
            //{
            //    ScrapedImages.Add(new(image.Image ?? Array.Empty<byte>(), image.URL ?? ""));
            //}
        }

        public string Greeting => "Welcome to Avalonia!";
    }
}