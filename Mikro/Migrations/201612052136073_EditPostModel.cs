namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPostModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Post_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Post_Id");
            AddForeignKey("dbo.AspNetUsers", "Post_Id", "dbo.Posts", "Id");
            DropColumn("dbo.Posts", "PlusCounter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "PlusCounter", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "Post_Id", "dbo.Posts");
            DropIndex("dbo.AspNetUsers", new[] { "Post_Id" });
            DropColumn("dbo.AspNetUsers", "Post_Id");
        }
    }
}
