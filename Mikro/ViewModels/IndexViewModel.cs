using Mikro.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mikro.ViewModels
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public int CommentCounter { get; set; }

        public IList<Comment> Comments { get; set; }
        public IList<Post> Posts { get; set; }
    }
}