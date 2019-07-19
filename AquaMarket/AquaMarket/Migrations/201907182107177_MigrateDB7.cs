namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plants", "Family_Id", c => c.Int());
            CreateIndex("dbo.Plants", "Family_Id");
            AddForeignKey("dbo.Plants", "Family_Id", "dbo.Families", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plants", "Family_Id", "dbo.Families");
            DropIndex("dbo.Plants", new[] { "Family_Id" });
            DropColumn("dbo.Plants", "Family_Id");
        }
    }
}
