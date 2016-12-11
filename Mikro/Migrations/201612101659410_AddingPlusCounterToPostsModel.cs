namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPlusCounterToPostsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PlusCounter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "PlusCounter");
        }
    }
}
