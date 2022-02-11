namespace ChilliSoft_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCheckboxField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingItems", "isSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeetingItems", "isSelected");
        }
    }
}
