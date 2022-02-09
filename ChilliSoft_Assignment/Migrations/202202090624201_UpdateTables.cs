namespace ChilliSoft_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeetingItems", "DueDate", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MeetingItems", "DueDate", c => c.DateTime(nullable: false));
        }
    }
}
