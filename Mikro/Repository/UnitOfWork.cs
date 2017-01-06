using Mikro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mikro.Repository
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public PostRepository Posts { get; private set; }
        public CommentRepository Comments { get; private set; }
        public PostPlusRepository PostPluses { get; private set; }
        public FollowingRepository Followings { get; private set; }
        public PostTagRepository PostTags { get; private set; }
        public CommentPlusRepository CommentPluses { get; set; }
        public TagRepository Tags { get; set; }

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