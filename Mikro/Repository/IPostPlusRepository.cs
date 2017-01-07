using System.Collections.Generic;
using Mikro.Models;

namespace Mikro.Repository
{
    public interface IPostPlusRepository
    {
        ICollection<PostPlus> GetPostPlusByUser(string userId);
        PostPlus GetPostPlusByPostIdAndUserId(int id, string userId);
        void Add(PostPlus postPlus);
        void Delete(PostPlus postPlus);
    }
}