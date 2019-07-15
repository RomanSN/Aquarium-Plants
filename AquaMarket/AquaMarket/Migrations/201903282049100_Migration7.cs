namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Plants", "Hight", c => c.Int());
            AlterColumn("dbo.Plants", "Temp", c => c.Int());
            AlterColumn("dbo.Plants", "Count", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Plants", "Count", c => c.Int(nullable: false));
            AlterColumn("dbo.Plants", "Temp", c => c.Int(nullable: false));
            AlterColumn("dbo.Plants", "Hight", c => c.Int(nullable: false));
        }
    }
}
