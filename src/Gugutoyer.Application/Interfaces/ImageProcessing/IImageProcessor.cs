﻿using Gugutoyer.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Interfaces.ImageProcessing
{
    public interface IImageProcessor
    {
        public byte[] CreateImage(PalavraDTO word);
    }
}
