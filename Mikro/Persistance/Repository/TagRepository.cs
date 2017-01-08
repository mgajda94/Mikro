using System.Linq;
using Mikro.Core.Models;
using Mikro.Core.Repositories;

namespace Mikro.Persistance.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Tag GetTagByName(string tagName)
        {
            return _context.Tags
                .SingleOrDefault(x => x.Name == tagName);
        }

        public void Add(Tag tag)
        {
            _context.Tags.Add(tag);
        }

        public void Delete(Tag tag)
        {
            _context.Tags.Remove(tag);
        }
    }
}