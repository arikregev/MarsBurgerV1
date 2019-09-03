namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Meals", "OrderItem_Id", "dbo.OrderItems");
            DropForeignKey("dbo.Addons", "OrderMeal_Id", "dbo.OrderMeals");
            DropForeignKey("dbo.OrderMeals", "OrderItem_Id", "dbo.OrderItems");
            DropForeignKey("dbo.Meals", "OrderItem_Id1", "dbo.OrderItems");
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Addons", new[] { "OrderMeal_Id" });
            DropIndex("dbo.Meals", new[] { "OrderItem_Id" });
            DropIndex("dbo.Meals", new[] { "OrderItem_Id1" });
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            DropIndex("dbo.OrderMeals", new[] { "OrderItem_Id" });
            AddColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "CreationTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Addons", "OrderMeal_Id");
            DropColumn("dbo.Meals", "OrderItem_Id");
            DropColumn("dbo.Meals", "OrderItem_Id1");
            DropTable("dbo.OrderItems");
            DropTable("dbo.OrderMeals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderMeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MealId = c.Int(nullable: false),
                        OrderItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Meals", "OrderItem_Id1", c => c.Int());
            AddColumn("dbo.Meals", "OrderItem_Id", c => c.Int());
            AddColumn("dbo.Addons", "OrderMeal_Id", c => c.Int());
            DropColumn("dbo.Orders", "CreationTime");
            DropColumn("dbo.Orders", "UserId");
            CreateIndex("dbo.OrderMeals", "OrderItem_Id");
            CreateIndex("dbo.OrderItems", "Order_Id");
            CreateIndex("dbo.Meals", "OrderItem_Id1");
            CreateIndex("dbo.Meals", "OrderItem_Id");
            CreateIndex("dbo.Addons", "OrderMeal_Id");
            AddForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.Meals", "OrderItem_Id1", "dbo.OrderItems", "Id");
            AddForeignKey("dbo.OrderMeals", "OrderItem_Id", "dbo.OrderItems", "Id");
            AddForeignKey("dbo.Addons", "OrderMeal_Id", "dbo.OrderMeals", "Id");
            AddForeignKey("dbo.Meals", "OrderItem_Id", "dbo.OrderItems", "Id");
        }
    }
}
