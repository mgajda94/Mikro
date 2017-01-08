using System.Collections.Generic;
using Mikro.Core.Models;

namespace Mikro.Core.Repositories
{
    public interface IPostTagRepository
    {
        IList<PostTag> GetPostTagsByTagId(int tagId);
        IList<PostTag> GetPostTagsByTag(Tag tag);
        void Add(PostTag postTag);
        void Delete(PostTag postTag);
    }
}