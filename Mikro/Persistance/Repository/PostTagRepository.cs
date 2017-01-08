using System.Collections.Generic;
using System.Linq;
using Mikro.Core.Models;
using Mikro.Core.Repositories;

namespace Mikro.Persistance.Repository
{
    public class PostTagRepository : IPostTagRepository
    {
        private readonly ApplicationDbContext _context;

        public PostTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PostTag> GetPostTagsByTagId(int tagId)
        {
            return _context.PostTag
                .Where(x => x.TagId == tagId)
                .ToList();
        }

        public IList<PostTag> GetPostTagsByTag(Tag tag)
        {
            return _context.PostTag
                .Where(x => x.Tag == tag)
                .ToList();
        }

        public void Add(PostTag postTag)
        {
            _context.PostTag.Add(postTag);
        }

        public void Delete(PostTag postTag)
        {
            _context.PostTag.Remove(postTag);
        }
    }
}