namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUsernameToPostModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Username");
        }
    }
}
