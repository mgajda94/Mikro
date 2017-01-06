using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mikro.Models;

namespace Mikro.Repository
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string userId, int tagId)
        {
            return _context.Followings
                .SingleOrDefault(x => x.UserId == userId && x.TagId == tagId);
        }

        public ICollection<Following> GetFollowings(string userId)
        {
            return _context.Followings
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Delete(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}