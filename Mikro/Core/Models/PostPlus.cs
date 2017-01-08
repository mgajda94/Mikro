using System.ComponentModel.DataAnnotations.Schema;

namespace Mikro.Core.Models
{
    public class PostPlus
    {
        public int Id { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public string UserId { get; set; }
    }
}