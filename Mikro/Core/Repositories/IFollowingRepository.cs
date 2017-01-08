using System.Collections.Generic;
using Mikro.Core.Models;

namespace Mikro.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, int tagId);
        ICollection<Following> GetFollowings(string userId);
        void Add(Following following);
        void Delete(Following following);
    }
}