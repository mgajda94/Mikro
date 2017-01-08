using System.Collections.Generic;
using Mikro.Core.Models;

namespace Mikro.Core.Repositories
{
    public interface IPostRepository
    {
        IList<Post> GetAllPosts();
        IList<Post> GetAllPostsByUsername(string username);
        Post GetPost(int postId);
        IList<Post> GetPostsById(int postId);
        void Add(Post post);
        void Delete(Post post);
    }
}