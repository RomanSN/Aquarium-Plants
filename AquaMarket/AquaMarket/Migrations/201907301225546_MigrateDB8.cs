namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Plants", "Family_Id", "dbo.Families");
            DropIndex("dbo.Plants", new[] { "Family_Id" });
            //DropColumn("dbo.Species", "FamilyId");
            //RenameColumn(table: "dbo.Species", name: "Family_Id", newName: "FamilyId");
            AddColumn("dbo.Families", "Name", c => c.String());
            AddColumn("dbo.Species", "Name", c => c.String());
            //AddForeignKey("dbo.Species", "FamilyId", "dbo.Families", "Id", cascadeDelete: true);
            DropColumn("dbo.Families", "FamilyName");
            //DropColumn("dbo.Plants", "Family_Id");
            DropColumn("dbo.Species", "SpeciesName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Species", "SpeciesName", c => c.String());
            //AddColumn("dbo.Plants", "Family_Id", c => c.Int());
            AddColumn("dbo.Families", "FamilyName", c => c.String());
            DropForeignKey("dbo.Species", "FamilyId", "dbo.Families");
            DropColumn("dbo.Species", "Name");
            DropColumn("dbo.Families", "Name");
            //RenameColumn(table: "dbo.Species", name: "FamilyId", newName: "Family_Id");
            AddColumn("dbo.Species", "FamilyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Plants", "Family_Id");
            AddForeignKey("dbo.Plants", "Family_Id", "dbo.Families", "Id");
        }
    }
}
