using Mikro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mikro.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IPostRepository Posts { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IPostPlusRepository PostPluses { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IPostTagRepository PostTags { get; private set; }
        public ICommentPlusRepository CommentPluses { get; private set; }
        public ITagRepository Tags { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Posts = new PostRepository(context);
            Comments = new CommentRepository(context);
            PostPluses = new PostPlusRepository(context);
            Followings = new FollowingRepository(context);
            PostTags = new PostTagRepository(context);
            CommentPluses = new CommentPlusRepository(context);
            Tags = new TagRepository(context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        
    }
}