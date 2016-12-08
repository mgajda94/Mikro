using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mikro.Models
{
    public class CommentPlus
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string UserId { get; set; }
    }
}