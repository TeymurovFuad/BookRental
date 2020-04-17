namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addednewfieldstouserregistrationview : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "disabled", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "disabled", c => c.Boolean(nullable: false));
        }
    }
}
