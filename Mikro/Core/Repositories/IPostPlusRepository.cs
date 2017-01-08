using System.Collections.Generic;
using Mikro.Core.Models;

namespace Mikro.Core.Repositories
{
    public interface IPostPlusRepository
    {
        ICollection<PostPlus> GetPostPlusByUser(string userId);
        PostPlus GetPostPlusByPostIdAndUserId(int id, string userId);
        void Add(PostPlus postPlus);
        void Delete(PostPlus postPlus);
    }
}