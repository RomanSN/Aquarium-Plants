namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "PlantId", c => c.Int(nullable: false));
            CreateIndex("dbo.Files", "PlantId");
            AddForeignKey("dbo.Files", "PlantId", "dbo.Plants", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "PlantId", "dbo.Plants");
            DropIndex("dbo.Files", new[] { "PlantId" });
            DropColumn("dbo.Files", "PlantId");
        }
    }
}
