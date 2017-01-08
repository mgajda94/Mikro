using System.ComponentModel.DataAnnotations;

namespace Mikro.Core.Dtos
{
    public class CommentDto
    {
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}