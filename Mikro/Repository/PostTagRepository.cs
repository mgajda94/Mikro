using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mikro.Models;

namespace Mikro.Repository
{
    public class PostTagRepository
    {
        private readonly ApplicationDbContext _context;

        public PostTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<PostTag> GetPostTagsByTagId(int tagId)
        {
            return _context.PostTag
                .Where(x => x.TagId == tagId)
                .ToList();
        }

        public ICollection<PostTag> GetPostTagsByTag(Tag tag)
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