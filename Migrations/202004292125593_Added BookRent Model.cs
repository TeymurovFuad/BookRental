namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBookRentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookRents",
                c => new
                    {
                        bookRentIdPK = c.Int(nullable: false, identity: true),
                        userRentId = c.Int(nullable: false),
                        bookRentId = c.Int(nullable: false),
                        startDate = c.DateTime(),
                        actualEndDate = c.DateTime(),
                        scheduledEndDate = c.DateTime(),
                        additionalCharge = c.Boolean(),
                        rentalPrice = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.bookRentIdPK);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookRents");
        }
    }
}
