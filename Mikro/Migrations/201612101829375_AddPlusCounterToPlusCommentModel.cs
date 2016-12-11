namespace Mikro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlusCounterToPlusCommentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "PlusCounter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "PlusCounter");
        }
    }
}
