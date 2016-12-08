using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mikro.Models
{
    public class PostPlus
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
    }
}