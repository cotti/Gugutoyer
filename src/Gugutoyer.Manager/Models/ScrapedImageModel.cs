using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Manager.Models
{
    public class ScrapedImageModel : IDisposable
    {
        private bool disposedValue;
        private MemoryStream stream;
        private Bitmap image;
        public ScrapedImageModel(byte[] image, string url)
        {
            this.stream = new MemoryStream(image);
            this.Url = url;
        }
        public Bitmap? Image 
        {
            get
            {
                if (image is not null) return image;
                try
                {
                    image = new Bitmap(stream);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Could not load the image due to an error. " + e.Message);
                    image = new Bitmap(@"avares:Gugutoyer.Manager.Assets.avalonia-logo.ico");
                }
                return image;
            }
        }
        public string? Url { get; init; }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    image?.Dispose();
                    image = null!;
                    stream.Dispose();
                    stream = null!;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
