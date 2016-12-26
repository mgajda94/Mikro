using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mikro.Dtos
{
    public class AddingPostDto
    {
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}