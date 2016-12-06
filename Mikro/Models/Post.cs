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

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public string Meta { get; set; }

        public List<ApplicationUser> PlusUsers { get; set; }

        public List<Comment> Comment { get; set; }

    }
}