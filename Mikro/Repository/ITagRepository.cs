using Mikro.Models;

namespace Mikro.Repository
{
    public interface ITagRepository
    {
        Tag GetTagByName(string tagName);
        void Add(Tag tag);
        void Delete(Tag tag);
    }
}