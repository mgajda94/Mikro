using System.Collections.Generic;
using Mikro.Models;

namespace Mikro.Repository
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, int tagId);
        ICollection<Following> GetFollowings(string userId);
        void Add(Following following);
        void Delete(Following following);
    }
}