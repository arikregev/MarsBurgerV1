namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrice2Addon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SideDishes", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SideDishes", "Price");
        }
    }
}
