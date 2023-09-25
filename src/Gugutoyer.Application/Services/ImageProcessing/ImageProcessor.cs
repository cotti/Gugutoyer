using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Gugutoyer.Application.Services.ImageProcessing
{
    public class ImageProcessor : IImageProcessor
    {
        private readonly IImageProvider _imageProvider;
        private readonly IList<TemplateDTO> _templates;

        private TemplateDTO? _activeTemplate;
        public ImageProcessor(IImageProvider imageProvider, IList<TemplateDTO> templates)
        {
            _imageProvider = imageProvider;
            _templates = templates;
        }

        public byte[] CreateImage(PalavraDTO word)
        {
            var rawSourceImg = _imageProvider.GetSourceImage(word);
            var tempPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()!.Location)!;

            MagickNET.SetTempDirectory(tempPath);

            using MagickImage imgsource = new(rawSourceImg);
            using MagickImage imgsourceEffect = new(imgsource.Clone());

            using MagickImageCollection imageCollection = new();
            using MagickImageCollection sourceEffectCollection = new();

            _activeTemplate = _templates[new Random().Next(_templates.Count)]!;

            using MagickImage imgtemplate = new(Path.Combine(tempPath, _activeTemplate.File!),
                new MagickReadSettings()
                {
                    BackgroundColor = MagickColors.Transparent
                });
            //Prepare the source material
            PrepareSource(imgsource, imgsourceEffect, imgtemplate.Width, imgtemplate.Height);

            //Apply effects for the template
            ApplyEffects(imgsourceEffect, word);

            imageCollection.Add(imgsourceEffect);

            imageCollection.Add(imgtemplate);

            using MagickImage res = new(imageCollection.Merge());
            res.Page = new MagickGeometry()
            {
                Width = imgtemplate.Width,
                Height = imgtemplate.Height,
            };
            res.Transparent(MagickColors.Transparent);
            return res.ToByteArray(MagickFormat.Jpg);
        }

        private static void PrepareSource(MagickImage imgsource, MagickImage imgsourceEffect, int templateWidth, int templateHeight)
        {
            //Background effect prep. 
            //Crop creates the base for the blurred background to fill the void
            imgsourceEffect.Crop(new MagickGeometry()
            {
                Less = true,
                Greater = false,
                IgnoreAspectRatio = false,
                FillArea = true,
                Width = (imgsourceEffect.Width > templateWidth) ? templateWidth : imgsourceEffect.Width,
                Height = (imgsourceEffect.Height > templateHeight) ? templateHeight : imgsourceEffect.Height,
            }, Gravity.Center);

            //Blur the image.
            imgsourceEffect.Blur(10, 10);

            //Fill the whole possible extension
            imgsourceEffect.Scale(new MagickGeometry()
            {
                Less = true,
                Greater = false,
                IgnoreAspectRatio = false,
                FillArea = true,
                Width = templateWidth,
                Height = templateHeight
            });

            //RePage() is required at this point.
            imgsourceEffect.RePage();

            //Prep the actual image.
            //Scale it to touch the vertical extremities. Let the blurred background take care of the horizontal space.
            imgsource.Scale(new MagickGeometry()
            {
                Greater = (imgsource.Width > templateWidth || imgsource.Height > templateHeight),
                Less = (imgsource.Width <= templateWidth && imgsource.Height <= templateHeight),
                //Greater = true,
                //Less = true,
                Height = templateHeight,
                IgnoreAspectRatio = false,
                FillArea = true,
            });

            //RePage()
            imgsource.RePage();

            //Cut any extra borders in the image. --Is this needed at this point? hmm...
            imgsource.Shave(
                (imgsource.Width > imgsourceEffect.Width) ? ((imgsource.Width - imgsourceEffect.Width) / 2) : 0,
                (imgsource.Height > imgsourceEffect.Height) ? ((imgsource.Height - imgsourceEffect.Height) / 2) : 0);

            //Glue the source above the blurred background.
            imgsourceEffect.Composite(imgsource, Gravity.Center, CompositeOperator.SrcOver);

            //Readapt the page and shave again. --Is this needed yet another time? hmm...
            imgsourceEffect.Page = new MagickGeometry()
            {
                X = (templateWidth - imgsourceEffect.Width > 0) ? ((templateWidth - imgsourceEffect.Width) / 2) : 0,
                Y = (templateHeight - imgsourceEffect.Height > 0) ? ((templateHeight - imgsourceEffect.Height) / 2) : 0
            };

            imgsourceEffect.Shave(
                (imgsourceEffect.Width > templateWidth) ? ((imgsourceEffect.Width - templateWidth) / 2) : 0,
                (imgsourceEffect.Height > templateHeight) ? ((imgsourceEffect.Height - templateHeight) / 2) : 0);
        }
        private void ApplyEffects(MagickImage image, PalavraDTO word)
        {
            if (_activeTemplate is not null && image is not null)
            {
                Draw(image, word, "Comic Sans MS", 60, "#662222FF", "#664433FF", "#111111FF", 3, 2, 2, true, Gravity.North, -5, true);
                Draw(image, word, "Comic Sans MS", 60, "#5A2B92FF", "#F0FFFFFF", "#800080FF", 2, 0, 0, true, Gravity.North, -5, true);
                Distort(image);
            }
        }

        private void Draw(MagickImage image, PalavraDTO word, string fontFamily, int maxFontPointSize, 
            string strokeColor, string fillColor, string borderColor, double strokeWidth, int compositeX,
            int compositeY, bool addDoGugu, Gravity gravity, double textKerning, bool heightLimiter)
        {
            var settings = new MagickReadSettings()
            {
                BackgroundColor = MagickColors.Transparent,
                FontFamily = fontFamily,
                StrokeColor = new MagickColor(strokeColor),
                FillColor = new MagickColor(fillColor),
                BorderColor = new MagickColor(borderColor),
                StrokeWidth = strokeWidth,
                TextGravity = gravity,
                Width = image.Width - _activeTemplate!.HorizontalCut,
                Height = (heightLimiter ? image.Height - _activeTemplate.VerticalCut : 100),
                Page = new MagickGeometry(image.Width, image.Height),
                Defines = new ImageMagick.Formats.CaptionReadDefines()
                {
                    MaxFontPointsize = maxFontPointSize
                },
                TextKerning = textKerning
            };
            //Caption does the resizing text magic.
            using var textImage = new MagickImage((heightLimiter ? "caption: " : "label: ")
                + word.Verbete![0].ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture)
                + word.Verbete![1..] + (addDoGugu ? " do Gugu" : ""), settings);

            image.Composite(textImage, gravity, compositeX, compositeY, CompositeOperator.SrcOver);
        }

        private static void Distort(MagickImage image)
        {
            //Left-Up, Left-Down, Right-Up, Right-Down
            //Initial X, Initial Y, Final X, Final Y 
            var enumerator = new double[] { 0, 0, 32, 18, 0, 405, 0, 370, 600, 0, 566, 18, 600, 405, 600, 370 };
            image.Distort(DistortMethod.Perspective, enumerator);
        }
    }
}
