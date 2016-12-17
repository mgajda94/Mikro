namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTagModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Tag_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Tag_Id");
            AddForeignKey("dbo.Posts", "Tag_Id", "dbo.Tags", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.Posts", new[] { "Tag_Id" });
            DropColumn("dbo.Posts", "Tag_Id");
        }
    }
}
