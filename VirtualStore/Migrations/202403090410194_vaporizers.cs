namespace VirtualStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vaporizers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vaporizers",
                c => new
                    {
                        vaporizerId = c.Int(nullable: false, identity: true),
                        VaporizerName = c.String(),
                        FlavourName = c.String(),
                        BuyingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerRatings = c.String(),
                        Profit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.vaporizerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vaporizers");
        }
    }
}
