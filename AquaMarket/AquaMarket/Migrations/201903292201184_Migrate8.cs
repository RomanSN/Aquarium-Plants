namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Plants", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Plants", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Plants", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.Plants", "Light", c => c.String(nullable: false));
            AlterColumn("dbo.Plants", "GrowthSpeed", c => c.String(nullable: false));
            AlterColumn("dbo.Plants", "Hight", c => c.Int(nullable: false));
            AlterColumn("dbo.Plants", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Plants", "Count", c => c.Int());
            AlterColumn("dbo.Plants", "Hight", c => c.Int());
            AlterColumn("dbo.Plants", "GrowthSpeed", c => c.String());
            AlterColumn("dbo.Plants", "Light", c => c.String());
            AlterColumn("dbo.Plants", "Location", c => c.String());
            AlterColumn("dbo.Plants", "Description", c => c.String());
            AlterColumn("dbo.Plants", "Name", c => c.String());
        }
    }
}
