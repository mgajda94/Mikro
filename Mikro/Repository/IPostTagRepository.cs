using System.Collections.Generic;
using Mikro.Models;

namespace Mikro.Repository
{
    public interface IPostTagRepository
    {
        IList<PostTag> GetPostTagsByTagId(int tagId);
        IList<PostTag> GetPostTagsByTag(Tag tag);
        void Add(PostTag postTag);
        void Delete(PostTag postTag);
    }
}