using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mikro.Core.Models;

namespace Mikro.Core.ViewModels
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

        public IEnumerable<Post> Posts { get; set; }
    }
}