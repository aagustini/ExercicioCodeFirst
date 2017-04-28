namespace EFCodeFirstApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Imagens : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ImageFile", c => c.Binary());
            AddColumn("dbo.Movies", "ImageMimeType", c => c.String());
            AddColumn("dbo.Movies", "ImageUrl", c => c.String());
            AlterColumn("dbo.Movies", "Title", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Movies", "Director", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Director", c => c.String());
            AlterColumn("dbo.Movies", "Title", c => c.String());
            DropColumn("dbo.Movies", "ImageUrl");
            DropColumn("dbo.Movies", "ImageMimeType");
            DropColumn("dbo.Movies", "ImageFile");
        }
    }
}
