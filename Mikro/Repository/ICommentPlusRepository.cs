using System.Collections.Generic;
using Mikro.Models;

namespace Mikro.Repository
{
    public interface ICommentPlusRepository
    {
        ICollection<CommentPlus> GetAllCommentPlusesByUser(string userId);
        CommentPlus GetCommentPlus(string userId, int commentId);
        void Add(CommentPlus commentPlus);
        void Delete(CommentPlus commentPlus);
    }
}