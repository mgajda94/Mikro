namespace Mikro.Core.Models
{
    public class CommentPlus
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

    }
}