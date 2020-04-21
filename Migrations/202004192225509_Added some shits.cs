namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedsomeshits : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MembershipTypes", "UserViewModel_userIdPK", "dbo.UserViewModels");
            DropIndex("dbo.MembershipTypes", new[] { "UserViewModel_userIdPK" });
            RenameColumn(table: "dbo.MembershipTypes", name: "UserViewModel_userIdPK", newName: "UserViewModel_Id");
            DropPrimaryKey("dbo.UserViewModels");
            AddColumn("dbo.UserViewModels", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserViewModels", "confirmPassword", c => c.String());
            AlterColumn("dbo.MembershipTypes", "UserViewModel_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserViewModels", "password", c => c.String());
            AddPrimaryKey("dbo.UserViewModels", "Id");
            CreateIndex("dbo.MembershipTypes", "UserViewModel_Id");
            AddForeignKey("dbo.MembershipTypes", "UserViewModel_Id", "dbo.UserViewModels", "Id");
            DropColumn("dbo.UserViewModels", "userIdPK");
            DropColumn("dbo.UserViewModels", "consfimrPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserViewModels", "consfimrPassword", c => c.String(nullable: false));
            AddColumn("dbo.UserViewModels", "userIdPK", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.MembershipTypes", "UserViewModel_Id", "dbo.UserViewModels");
            DropIndex("dbo.MembershipTypes", new[] { "UserViewModel_Id" });
            DropPrimaryKey("dbo.UserViewModels");
            AlterColumn("dbo.UserViewModels", "password", c => c.String(nullable: false));
            AlterColumn("dbo.MembershipTypes", "UserViewModel_Id", c => c.Int());
            DropColumn("dbo.UserViewModels", "confirmPassword");
            DropColumn("dbo.UserViewModels", "Id");
            AddPrimaryKey("dbo.UserViewModels", "userIdPK");
            RenameColumn(table: "dbo.MembershipTypes", name: "UserViewModel_Id", newName: "UserViewModel_userIdPK");
            CreateIndex("dbo.MembershipTypes", "UserViewModel_userIdPK");
            AddForeignKey("dbo.MembershipTypes", "UserViewModel_userIdPK", "dbo.UserViewModels", "userIdPK");
        }
    }
}
