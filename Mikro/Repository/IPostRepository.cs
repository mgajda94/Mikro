using System.Collections.Generic;
using Mikro.Models;

namespace Mikro.Repository
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