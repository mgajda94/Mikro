using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mikro.Core.Models;

namespace Mikro.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostPlus> PostPluses { get; set; }
        public DbSet<CommentPlus> CommentPluses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTag { get; set; }
        public DbSet<Following> Followings { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostTag>().HasKey(x => new { x.PostId, x.TagId });
            modelBuilder.Entity<Tag>()
                .HasMany(x => x.PostTag)
                .WithRequired()
                .HasForeignKey(x => x.TagId);
            modelBuilder.Entity<Post>()
                .HasMany(x => x.PostTag)
                .WithRequired()
                .HasForeignKey(x => x.PostId);

            modelBuilder.Entity<Comment>()
                .HasRequired<Post>(x => x.Post)
                .WithMany(x => x.Comments);

            modelBuilder.Entity<Following>().HasKey(x => new {x.TagId, x.UserId});
            modelBuilder.Entity<Tag>()
                .HasMany(x => x.Following)
                .WithRequired()
                .HasForeignKey(x => x.TagId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Following)
                .WithRequired()
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}