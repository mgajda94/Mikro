namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirstMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentPlus", "CommentId", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropColumn("dbo.Comments", "Id");
            RenameColumn(table: "dbo.Comments", name: "PostId", newName: "Id");
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.Comments", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "PostId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Comments", "PostId");
            CreateIndex("dbo.Comments", "Id");
            AddForeignKey("dbo.CommentPlus", "CommentId", "dbo.Comments", "PostId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentPlus", "CommentId", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "Id" });
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.Comments", "PostId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Comments", "Id");
            RenameColumn(table: "dbo.Comments", name: "Id", newName: "PostId");
            AddColumn("dbo.Comments", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Comments", "PostId");
            AddForeignKey("dbo.CommentPlus", "CommentId", "dbo.Comments", "Id", cascadeDelete: true);
        }
    }
}
