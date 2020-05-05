namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedrentaldurationcolumntoBookRenttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookRents", "rentalDuration", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookRents", "rentalDuration");
        }
    }
}
