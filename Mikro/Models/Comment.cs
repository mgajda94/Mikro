using System;
using System.ComponentModel.DataAnnotations;

namespace Mikro.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public int PostId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        public DateTime? Modifed { get; set; }
        [Required]
        public string Content { get; set; }

        public string Meta { get; set; }
        public int PlusCounter { get; set; }
    }
}