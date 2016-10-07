using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Puppr.API.Models
{
    public class PetPhoto
    {
        public int PetPhotoId { get; set; }
        public int PetId { get; set; }
        public string Url { get; set; }

        public virtual Pet Pet { get; set; }
    }
}