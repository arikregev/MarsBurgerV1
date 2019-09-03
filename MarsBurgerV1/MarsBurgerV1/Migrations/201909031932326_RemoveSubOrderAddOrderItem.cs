namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSubOrderAddOrderItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStatusID", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "UserId", c => c.String(nullable: false));
            DropTable("dbo.SubmitedOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubmitedOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.Orders", "OrderStatusID");
        }
    }
}
