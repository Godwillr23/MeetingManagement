namespace ChilliSoft_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusTableAndUpdateStaffTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusItemId = c.Int(nullable: false, identity: true),
                        StatusItem = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StatusItemId);
            
            AlterColumn("dbo.Staffs", "Firstname", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "Lastname", c => c.String(nullable: false));
            DropColumn("dbo.Staffs", "ItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Staffs", "ItemId", c => c.Int(nullable: false));
            AlterColumn("dbo.Staffs", "Lastname", c => c.String());
            AlterColumn("dbo.Staffs", "Firstname", c => c.String());
            DropTable("dbo.Status");
        }
    }
}
