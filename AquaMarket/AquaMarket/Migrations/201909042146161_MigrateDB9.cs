namespace AquaMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Section = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Likes = c.Int(),
                        FileId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Files", t => t.FileId)
                .Index(t => t.FileId);
            
            AddColumn("dbo.Files", "ArticleId", c => c.Int());
            AlterColumn("dbo.Files", "PlantId", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "FileId", "dbo.Files");
            DropIndex("dbo.Articles", new[] { "FileId" });
            AlterColumn("dbo.Files", "PlantId", c => c.Int(nullable: false));
            DropColumn("dbo.Files", "ArticleId");
            DropTable("dbo.Articles");
        }
    }
}
