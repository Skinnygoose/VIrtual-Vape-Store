namespace VirtualStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suppliers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                        SupplierAddress = c.String(),
                        SupplierEmail = c.String(),
                    })
                .PrimaryKey(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.suppliers");
        }
    }
}
