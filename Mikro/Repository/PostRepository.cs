using Mikro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace Mikro.Repository
{
    public class PostRepository : IRepository<Post>
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public void Add(Post entity)
        {
            _context.Posts.Add(entity);
        }

        public void Delete(Post entity)
        {
            _context.Posts.Remove(entity);
        }

        public Post GetDetail(Func<Post, bool> predicate)
        {
            return _context.Posts.FirstOrDefault(predicate);
        }

        public IEnumerable<Post> GetOverview(Func<Post, bool> predicate = null)
        {
            if (predicate != null)
                return _context.Posts.Where(predicate).OrderByDescending(x=>x.PostedOn);
            return _context.Posts.OrderByDescending(x=>x.PostedOn);
        }

        public Post GetById(int id)
        {
            return _context.Posts.Single(x => x.Id == id);
        }

        internal void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Post Select(Expression<Func<Post, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}