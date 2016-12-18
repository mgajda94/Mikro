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
        public IList<PostPlus> Plus { get; set; }
        public IList<Comment> Comments { get; set; }
        public ICollection<Post> Posts { get; set; }
        public Tag Tag { get; set; }

        public IndexViewModel()
        {
            this.Posts = new List<Post>();
        }
    }
}