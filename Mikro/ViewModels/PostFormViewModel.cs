using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mikro.Models;
using System.ComponentModel.DataAnnotations;

namespace Mikro.ViewModels
{
    public class PostFormViewModel
    {  
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public int PlusCounter { get; set; }

        public IList<Post> Posts { get; set; }
    }
}