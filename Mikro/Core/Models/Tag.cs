using System.Collections.Generic;

namespace Mikro.Core.Models
{
    public class Tag
    {       
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PostTag> PostTag { get; set; }
        public virtual ICollection<Following> Following { get; set; }
    }
}