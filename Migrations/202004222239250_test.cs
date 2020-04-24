namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MembershipTypes", "UserViewModel_Id", "dbo.UserViewModels");
            DropIndex("dbo.MembershipTypes", new[] { "UserViewModel_Id" });
            DropColumn("dbo.MembershipTypes", "UserViewModel_Id");
            DropTable("dbo.UserViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        email = c.String(nullable: false),
                        password = c.String(),
                        confirmPassword = c.String(),
                        userMemTypeId = c.Int(nullable: false),
                        fname = c.String(nullable: false),
                        lname = c.String(nullable: false),
                        phone = c.String(nullable: false),
                        bdate = c.DateTime(nullable: false),
                        disabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MembershipTypes", "UserViewModel_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.MembershipTypes", "UserViewModel_Id");
            AddForeignKey("dbo.MembershipTypes", "UserViewModel_Id", "dbo.UserViewModels", "Id");
        }
    }
}
