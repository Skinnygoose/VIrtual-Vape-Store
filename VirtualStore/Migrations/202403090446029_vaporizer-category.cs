namespace VirtualStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vaporizercategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vaporizers", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vaporizers", "CategoryId");
            AddForeignKey("dbo.Vaporizers", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vaporizers", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Vaporizers", new[] { "CategoryId" });
            DropColumn("dbo.Vaporizers", "CategoryId");
        }
    }
}
