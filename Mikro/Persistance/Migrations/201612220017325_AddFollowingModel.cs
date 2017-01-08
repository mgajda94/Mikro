namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        TagId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TagId, t.UserId })
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TagId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "TagId", "dbo.Tags");
            DropIndex("dbo.Followings", new[] { "UserId" });
            DropIndex("dbo.Followings", new[] { "TagId" });
            DropTable("dbo.Followings");
        }
    }
}
