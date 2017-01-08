using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mikro.Core.Models
{
    public class PostTag
    {
        public int Id { get; set; }
        [Key, Column(Order = 0)]
        public int PostId { get; set; }
        [Key, Column(Order = 1)]
        public int TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}