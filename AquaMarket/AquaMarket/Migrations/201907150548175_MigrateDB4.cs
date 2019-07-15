namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plants", "MinTemp", c => c.Int(nullable: false));
            AddColumn("dbo.Plants", "MaxTemp", c => c.Int(nullable: false));
            AddColumn("dbo.Plants", "MinPh", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Plants", "MaxPh", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Plants", "MinGh", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Plants", "MaxGh", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Plants", "Temp");
            DropColumn("dbo.Plants", "Ph");
            DropColumn("dbo.Plants", "Gh");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plants", "Gh", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Plants", "Ph", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Plants", "Temp", c => c.Int(nullable: false));
            DropColumn("dbo.Plants", "MaxGh");
            DropColumn("dbo.Plants", "MinGh");
            DropColumn("dbo.Plants", "MaxPh");
            DropColumn("dbo.Plants", "MinPh");
            DropColumn("dbo.Plants", "MaxTemp");
            DropColumn("dbo.Plants", "MinTemp");
        }
    }
}
