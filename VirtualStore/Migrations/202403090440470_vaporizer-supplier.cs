namespace VirtualStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vaporizersupplier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vaporizers", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vaporizers", "SupplierId");
            AddForeignKey("dbo.Vaporizers", "SupplierId", "dbo.suppliers", "SupplierId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vaporizers", "SupplierId", "dbo.suppliers");
            DropIndex("dbo.Vaporizers", new[] { "SupplierId" });
            DropColumn("dbo.Vaporizers", "SupplierId");
        }
    }
}
