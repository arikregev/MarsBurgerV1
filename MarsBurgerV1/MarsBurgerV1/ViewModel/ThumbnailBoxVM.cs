using MarsBurgerV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.ViewModel
{
    public class ThumbnailBoxVM
    {
        public IEnumerable<Thumbnail> Thumbnails { get; set; }
    }
}