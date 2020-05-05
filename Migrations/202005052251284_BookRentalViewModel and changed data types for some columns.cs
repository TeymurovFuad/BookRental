namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookRentalViewModelandchangeddatatypesforsomecolumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "availability", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "availability", c => c.String(nullable: false));
        }
    }
}
