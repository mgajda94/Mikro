using System.Collections.Generic;
using System.Linq;
using Mikro.Models;

namespace Mikro.Repository
{
    public class PostPlusRepository
    {
        private readonly ApplicationDbContext _context;

        public PostPlusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<PostPlus> GetPostPlusByUser(string userId)
        {
            return _context.PostPluses
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public PostPlus GetPostPlusByPostIdAndUserId(int id, string userId)
        {
            return _context.PostPluses.Single(x => x.PostId == id && x.UserId == userId);
        }

        public void Add(PostPlus postPlus)
        {
            _context.PostPluses.Add(postPlus);
        }

        public void Delete(PostPlus postPlus)
        {
            _context.PostPluses.Remove(postPlus);
        }
    }
}