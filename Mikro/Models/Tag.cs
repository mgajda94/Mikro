﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mikro.Models
{
    public class Tag
    {       
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int> PostsId { get; set; }

        public Tag()
        {
            this.PostsId = new List<int>();
        }
    }
}