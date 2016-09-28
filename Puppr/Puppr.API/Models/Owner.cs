using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Puppr.API.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string Photo { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}