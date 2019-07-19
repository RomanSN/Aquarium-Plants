namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "PlantId", "dbo.Plants");
            DropForeignKey("dbo.Purchases", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Files", new[] { "PlantId" });
            DropIndex("dbo.Purchases", new[] { "CustomerId" });
            AddColumn("dbo.Plants", "PlantType", c => c.String(nullable: false));
            AddColumn("dbo.Plants", "FileId", c => c.Int());
            CreateIndex("dbo.Plants", "FileId");
            AddForeignKey("dbo.Plants", "FileId", "dbo.Files", "Id");
            DropColumn("dbo.Plants", "Type");
            DropColumn("dbo.Purchases", "CustomerId");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Purchases", "CustomerId", c => c.Int());
            AddColumn("dbo.Plants", "Type", c => c.String(nullable: false));
            DropForeignKey("dbo.Plants", "FileId", "dbo.Files");
            DropIndex("dbo.Plants", new[] { "FileId" });
            DropColumn("dbo.Plants", "FileId");
            DropColumn("dbo.Plants", "PlantType");
            CreateIndex("dbo.Purchases", "CustomerId");
            CreateIndex("dbo.Files", "PlantId");
            AddForeignKey("dbo.Purchases", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Files", "PlantId", "dbo.Plants", "Id", cascadeDelete: true);
        }
    }
}
