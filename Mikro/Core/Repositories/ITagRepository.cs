using Mikro.Core.Models;

namespace Mikro.Core.Repositories
{
    public interface ITagRepository
    {
        Tag GetTagByName(string tagName);
        void Add(Tag tag);
        void Delete(Tag tag);
    }
}