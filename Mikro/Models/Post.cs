using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mikro.Models
{
    public class Post
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public DateTime PostedOn { get; set; }

        public DateTime? Modifed { get; set; }

        public string Content { get; set; }

        public string Meta { get; set; }

        public int PlusCounter { get; set; }

        public IList<Comment> Comment { get; set; }

    }
}