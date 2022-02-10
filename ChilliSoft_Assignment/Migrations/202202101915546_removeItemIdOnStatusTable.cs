namespace ChilliSoft_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeItemIdOnStatusTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Status", "ItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Status", "ItemId", c => c.Int(nullable: false));
        }
    }
}
