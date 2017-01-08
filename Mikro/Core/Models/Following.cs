using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mikro.Core.Models
{
    public class Following
    {
        [Key]
        [Column(Order = 1)]
        public int TagId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }

        public Tag Tag { get; set; }
        public ApplicationUser User { get; set; }
    }
}