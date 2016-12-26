using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mikro.Dtos
{
    public class CommentDto
    {
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}