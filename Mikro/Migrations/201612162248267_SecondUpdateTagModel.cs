namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondUpdateTagModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.Posts", new[] { "Tag_Id" });
            DropColumn("dbo.Posts", "Tag_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Tag_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Tag_Id");
            AddForeignKey("dbo.Posts", "Tag_Id", "dbo.Tags", "Id");
        }
    }
}
