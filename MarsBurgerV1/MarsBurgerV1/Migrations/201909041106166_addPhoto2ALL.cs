namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhoto2ALL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addons", "ImageUrl", c => c.String(nullable: false));
            AddColumn("dbo.Drinks", "ImageUrl", c => c.String(nullable: false));
            AddColumn("dbo.SideDishes", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SideDishes", "ImageUrl");
            DropColumn("dbo.Drinks", "ImageUrl");
            DropColumn("dbo.Addons", "ImageUrl");
        }
    }
}
