namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfieldstomembershiptable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "fname", c => c.String());
            AddColumn("dbo.AspNetUsers", "lname", c => c.String());
            AddColumn("dbo.AspNetUsers", "phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "bdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "disabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "membershipTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "membershipTypeId");
            DropColumn("dbo.AspNetUsers", "disabled");
            DropColumn("dbo.AspNetUsers", "bdate");
            DropColumn("dbo.AspNetUsers", "phone");
            DropColumn("dbo.AspNetUsers", "lname");
            DropColumn("dbo.AspNetUsers", "fname");
        }
    }
}
