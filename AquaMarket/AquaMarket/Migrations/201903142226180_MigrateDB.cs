namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "FilePath", c => c.String(nullable: true));
            //AddColumn("dbo.Purchases", "FilePath", c => c.String(nullable: true));
        }

        public override void Down()
        {
            DropColumn("dbo.Purchases", "FilePath");
            
        }
    }
}
