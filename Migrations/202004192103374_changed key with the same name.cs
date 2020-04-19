namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedkeywiththesamename : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserViewModels",
                c => new
                    {
                        userIdPK = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        consfimrPassword = c.String(nullable: false),
                        userMemTypeId = c.Int(nullable: false),
                        fname = c.String(nullable: false),
                        lname = c.String(nullable: false),
                        phone = c.String(nullable: false),
                        bdate = c.DateTime(nullable: false),
                        disabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.userIdPK);
            
            AddColumn("dbo.MembershipTypes", "UserViewModel_userIdPK", c => c.Int());
            AddColumn("dbo.AspNetUsers", "identityId", c => c.Int(nullable: false));
            CreateIndex("dbo.MembershipTypes", "UserViewModel_userIdPK");
            AddForeignKey("dbo.MembershipTypes", "UserViewModel_userIdPK", "dbo.UserViewModels", "userIdPK");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MembershipTypes", "UserViewModel_userIdPK", "dbo.UserViewModels");
            DropIndex("dbo.MembershipTypes", new[] { "UserViewModel_userIdPK" });
            DropColumn("dbo.AspNetUsers", "identityId");
            DropColumn("dbo.MembershipTypes", "UserViewModel_userIdPK");
            DropTable("dbo.UserViewModels");
        }
    }
}
