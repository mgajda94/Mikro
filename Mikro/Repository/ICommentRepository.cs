using System.Collections.Generic;
using Mikro.Models;

namespace Mikro.Repository
{
    public interface ICommentRepository
    {
        IList<Comment> GetCommentsByPostId(int PostId);
        IList<Comment> GetComments();
        Comment GetCommentById(int id);
        void Add(Comment comment);
        void Delete(Comment comment);
    }
}