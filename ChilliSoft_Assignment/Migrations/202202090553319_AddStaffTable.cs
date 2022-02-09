namespace ChilliSoft_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStaffTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        Firstname = c.String(),
                        Lastname = c.String(),
                    })
                .PrimaryKey(t => t.StaffId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Staffs");
        }
    }
}
