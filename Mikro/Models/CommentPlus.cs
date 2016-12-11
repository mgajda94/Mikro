using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mikro.Models
{
    public class CommentPlus
    {
        public int Id { get; set; }

        [ForeignKey("Comment")]
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public string UserId { get; set; }
    }
}