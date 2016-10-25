using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Puppr.API.Models
{
    public class OwnerPhoto
    {
        public int OwnerPhotoId { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }

        public virtual Owner Owner { get; set; }
    }
}