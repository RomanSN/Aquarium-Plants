namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration71 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Plants", "Temp", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Plants", "Temp", c => c.Int());
        }
    }
}
