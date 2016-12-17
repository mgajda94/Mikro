using Mikro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mikro.ViewModels
{
    public class TagViewModel
    {
        public ICollection<Post> Posts { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}