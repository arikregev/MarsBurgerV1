namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DrinkAndAddon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addons", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Drinks", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drinks", "Price");
            DropColumn("dbo.Addons", "Price");
        }
    }
}
