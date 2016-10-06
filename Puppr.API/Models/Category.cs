using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Puppr.API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Battle> Battles { get; set; }
    }
}