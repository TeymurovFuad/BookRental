namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeddatatypeofpricecolumnfrombooltodouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookRents", "userRentId", c => c.String(nullable: false));
            AlterColumn("dbo.BookRents", "rentalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookRents", "rentalPrice", c => c.Boolean(nullable: false));
            AlterColumn("dbo.BookRents", "userRentId", c => c.Int(nullable: false));
        }
    }
}
