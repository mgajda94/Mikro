using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mikro.Models;

namespace Mikro.Repository
{
    public class CommentPlusRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentPlusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<CommentPlus> GetAllCommentPlusesByUser(string userId)
        {
            return _context.CommentPluses
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public CommentPlus GetCommentPlus(string userId, int commentId)
        {
            return _context.CommentPluses
                .SingleOrDefault(x => x.CommentId == commentId && x.UserId == userId);
        }

        public void Add(CommentPlus commentPlus)
        {
            _context.CommentPluses.Add(commentPlus);
        }

        public void Delete(CommentPlus commentPlus)
        {
            _context.CommentPluses.Remove(commentPlus);
        }
    }
}