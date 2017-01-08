using System.Collections.Generic;
using Mikro.Core.Models;

namespace Mikro.Core.ViewModels
{
    public class TagViewModel
    {
        public ICollection<Post> Posts { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}