 namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Plants_Purchases", "PlantId", "dbo.Plants");
            DropForeignKey("dbo.Plants_Purchases", "PurchaseId", "dbo.Purchases");
            DropIndex("dbo.Plants_Purchases", new[] { "PlantId" });
            DropIndex("dbo.Plants_Purchases", new[] { "PurchaseId" });
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FamilyName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpeciesName = c.String(),
                        Description = c.String(),
                        FamilyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Families", t => t.FamilyId, cascadeDelete: true)
                .Index(t => t.FamilyId);
            
            AddColumn("dbo.Plants", "PlantName", c => c.String(nullable: false));
            AddColumn("dbo.Plants", "Usage", c => c.String(nullable: false));
            AddColumn("dbo.Plants", "Coloration", c => c.String(nullable: false));
            AddColumn("dbo.Plants", "Сomplexity", c => c.String(nullable: false));
            AddColumn("dbo.Plants", "Area", c => c.String(nullable: false));
            AddColumn("dbo.Plants", "OriginCountry", c => c.String(nullable: false));
            AddColumn("dbo.Plants", "Type", c => c.String(nullable: false));
            AddColumn("dbo.Plants", "SpeciesId", c => c.Int());
            AddColumn("dbo.Plants", "Family_Id", c => c.Int());
            AddColumn("dbo.Plants", "Purchase_Id", c => c.Int());
            CreateIndex("dbo.Plants", "SpeciesId");
            CreateIndex("dbo.Plants", "Family_Id");
            CreateIndex("dbo.Plants", "Purchase_Id");
            AddForeignKey("dbo.Plants", "SpeciesId", "dbo.Species", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Plants", "Family_Id", "dbo.Families", "Id");
            AddForeignKey("dbo.Plants", "Purchase_Id", "dbo.Purchases", "Id");
            DropColumn("dbo.Plants", "Name");
            DropColumn("dbo.Plants", "Kh");
            DropColumn("dbo.Plants", "Count");
            DropColumn("dbo.Plants", "Price");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Plants_Purchases",
                c => new
                    {
                        PlantId = c.Int(nullable: false),
                        PurchaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlantId, t.PurchaseId });
            
            AddColumn("dbo.Plants", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Plants", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Plants", "Kh", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Plants", "Name", c => c.String(nullable: false));
            DropForeignKey("dbo.Plants", "Purchase_Id", "dbo.Purchases");
            DropForeignKey("dbo.Plants", "Family_Id", "dbo.Families");
            DropForeignKey("dbo.Plants", "SpeciesId", "dbo.Species");
            DropForeignKey("dbo.Species", "FamilyId", "dbo.Families");
            DropIndex("dbo.Species", new[] { "FamilyId" });
            DropIndex("dbo.Plants", new[] { "Purchase_Id" });
            DropIndex("dbo.Plants", new[] { "Family_Id" });
            DropIndex("dbo.Plants", new[] { "SpeciesId" });
            DropColumn("dbo.Plants", "Purchase_Id");
            DropColumn("dbo.Plants", "Family_Id");
            DropColumn("dbo.Plants", "SpeciesId");
            DropColumn("dbo.Plants", "Type");
            DropColumn("dbo.Plants", "OriginCountry");
            DropColumn("dbo.Plants", "Area");
            DropColumn("dbo.Plants", "Сomplexity");
            DropColumn("dbo.Plants", "Coloration");
            DropColumn("dbo.Plants", "Usage");
            DropColumn("dbo.Plants", "PlantName");
            DropTable("dbo.Species");
            DropTable("dbo.Families");
            CreateIndex("dbo.Plants_Purchases", "PurchaseId");
            CreateIndex("dbo.Plants_Purchases", "PlantId");
            AddForeignKey("dbo.Plants_Purchases", "PurchaseId", "dbo.Purchases", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Plants_Purchases", "PlantId", "dbo.Plants", "Id", cascadeDelete: true);
        }
    }
}
