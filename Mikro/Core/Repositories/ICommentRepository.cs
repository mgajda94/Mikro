using System.Collections.Generic;
using Mikro.Core.Models;

namespace Mikro.Core.Repositories
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