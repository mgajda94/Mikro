namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostPlusAndCommentPlusModels2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentPlus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostPlus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PostPlus");
            DropTable("dbo.CommentPlus");
        }
    }
}
