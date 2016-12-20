using Mikro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mikro.ViewModels
{
    public class PostFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string PostedContent { get; set; }

        public string UserId { get; set; }

        public DateTime PostedOn { get; set; }

        public IList<PostPlus> Plus { get; set; }

        public IList<Comment> Comments { get; set; }

        public IList<Post> Posts { get; set; }
    }
}