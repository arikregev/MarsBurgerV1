namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addItemOrigID2OrderItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "ItemOrigID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "ItemOrigID");
        }
    }
}
