namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostPlusAndCommentPlusModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.AspNetUsers", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.AspNetUsers", new[] { "Post_Id" });
            DropColumn("dbo.Comments", "Meta");
            DropColumn("dbo.Comments", "PlusCounter");
            DropColumn("dbo.AspNetUsers", "Post_Id");
            DropColumn("dbo.Posts", "Meta");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Meta", c => c.String());
            AddColumn("dbo.AspNetUsers", "Post_Id", c => c.Int());
            AddColumn("dbo.Comments", "PlusCounter", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "Meta", c => c.String());
            CreateIndex("dbo.AspNetUsers", "Post_Id");
            CreateIndex("dbo.Comments", "PostId");
            AddForeignKey("dbo.AspNetUsers", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
