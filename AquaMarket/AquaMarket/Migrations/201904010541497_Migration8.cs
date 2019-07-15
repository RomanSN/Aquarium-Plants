namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "FileType", c => c.String());
            DropColumn("dbo.Files", "FileBytes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "FileBytes", c => c.Binary());
            DropColumn("dbo.Files", "FileType");
        }
    }
}
