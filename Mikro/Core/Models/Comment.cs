using System;
using System.ComponentModel.DataAnnotations;

namespace Mikro.Core.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string UserName { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        public int PlusCounter { get; set; }

        public DateTime? Modifed { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [DataType(DataType.MultilineText)]
        public string PostedContent { get; set; }
        
        [Required]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

    }
}