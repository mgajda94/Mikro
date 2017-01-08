using Mikro.Core.Repositories;

namespace Mikro.Core
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        IPostPlusRepository PostPluses { get; }
        IFollowingRepository Followings { get; }
        IPostTagRepository PostTags { get; }
        ICommentPlusRepository CommentPluses { get; }
        ITagRepository Tags { get; }
        void SaveChanges();
    }
}