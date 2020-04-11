namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columnnamechangedfromPricetoPrice : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Books", name: "Price", newName: "Price");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Books", name: "Price", newName: "Price");
        }
    }
}
