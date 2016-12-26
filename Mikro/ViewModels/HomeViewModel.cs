using Mikro.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mikro.ViewModels
{
    public class HomeViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [DataType(DataType.MultilineText)]
        public string PostedContent { get; set; }

        public Following Following { get; set; }
        public bool isFollowing { get; set; }
        public int CommentCounter { get; set; }
        public IList<PostPlus> Plus { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<Post> Posts { get; set; }
        public Tag Tag { get; set; }

        public HomeViewModel()
        {
                Posts = new List<Post>();
        }
    }
}