namespace ChilliSoft_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStatusTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Status", "ItemId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Status", "ItemId");
        }
    }
}
