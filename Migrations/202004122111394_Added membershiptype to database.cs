namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedmembershiptypetodatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        membershipTypesIdPK = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        SignUpFee = c.Int(nullable: false),
                        chargeRateOneMonth = c.Byte(nullable: false),
                        chargeRateSixMonth = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.membershipTypesIdPK);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MembershipTypes");
        }
    }
}
