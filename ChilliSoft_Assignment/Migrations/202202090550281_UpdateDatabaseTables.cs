namespace ChilliSoft_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabaseTables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeetingItems", "MeetingItemName", c => c.String(nullable: false));
            AlterColumn("dbo.MeetingItems", "ActionBy", c => c.String(nullable: false));
            AlterColumn("dbo.MeetingItems", "ItemDescription", c => c.String(nullable: false));
            AlterColumn("dbo.MeetingItems", "ItemStatus", c => c.String(nullable: false));
            AlterColumn("dbo.Meetings", "MeetingCode", c => c.String(nullable: false));
            AlterColumn("dbo.Meetings", "MeetingDate", c => c.String(nullable: false));
            AlterColumn("dbo.Meetings", "MeetingTime", c => c.String(nullable: false));
            AlterColumn("dbo.MeetingTypes", "MeetingName", c => c.String(nullable: false));
            DropColumn("dbo.Meetings", "MeetingTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meetings", "MeetingTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.MeetingTypes", "MeetingName", c => c.String());
            AlterColumn("dbo.Meetings", "MeetingTime", c => c.String());
            AlterColumn("dbo.Meetings", "MeetingDate", c => c.String());
            AlterColumn("dbo.Meetings", "MeetingCode", c => c.String());
            AlterColumn("dbo.MeetingItems", "ItemStatus", c => c.String());
            AlterColumn("dbo.MeetingItems", "ItemDescription", c => c.String());
            AlterColumn("dbo.MeetingItems", "ActionBy", c => c.String());
            AlterColumn("dbo.MeetingItems", "MeetingItemName", c => c.String());
        }
    }
}
