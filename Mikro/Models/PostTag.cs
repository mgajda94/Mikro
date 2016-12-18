using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mikro.Models
{
    public class PostTag
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}