namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingForeignKeyToCommentPlusandPostPlus : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CommentPlus", "CommentId");
            CreateIndex("dbo.PostPlus", "PostId");
            AddForeignKey("dbo.CommentPlus", "CommentId", "dbo.Comments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostPlus", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostPlus", "PostId", "dbo.Posts");
            DropForeignKey("dbo.CommentPlus", "CommentId", "dbo.Comments");
            DropIndex("dbo.PostPlus", new[] { "PostId" });
            DropIndex("dbo.CommentPlus", new[] { "CommentId" });
        }
    }
}
