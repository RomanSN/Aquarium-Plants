namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Plants", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plants", "Photo", c => c.String());
        }
    }
}
