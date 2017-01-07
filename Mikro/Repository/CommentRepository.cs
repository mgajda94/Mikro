using System.Collections.Generic;
using System.Linq;
using Mikro.Models;

namespace Mikro.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Comment> GetCommentsByPostId(int postId)
        {
            return _context.Comments
                .Where(x => x.PostId == postId)
                .OrderByDescending(x => x.PostedOn)
                .ToList();
        }


        public IList<Comment> GetComments()
        {
            return _context.Comments.ToList();
        }

        public Comment GetCommentById(int id)
        {
            return _context.Comments
                .SingleOrDefault(x => x.Id == id);
        }

        public void Add(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public void Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
        }
    }
}