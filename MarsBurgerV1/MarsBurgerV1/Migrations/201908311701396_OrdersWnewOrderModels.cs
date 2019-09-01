namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersWnewOrderModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "AddonId", "dbo.Addons");
            DropForeignKey("dbo.Orders", "DrinkId", "dbo.Drinks");
            DropForeignKey("dbo.Orders", "MealId", "dbo.Meals");
            DropIndex("dbo.Orders", new[] { "MealId" });
            DropIndex("dbo.Orders", new[] { "AddonId" });
            DropIndex("dbo.Orders", new[] { "DrinkId" });
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.OrderMeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MealId = c.Int(nullable: false),
                        OrderItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderItems", t => t.OrderItem_Id)
                .Index(t => t.OrderItem_Id);
            
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
            
            AddColumn("dbo.Addons", "OrderMeal_Id", c => c.Int());
            AddColumn("dbo.Meals", "OrderItem_Id", c => c.Int());
            AddColumn("dbo.Meals", "OrderItem_Id1", c => c.Int());
            CreateIndex("dbo.Addons", "OrderMeal_Id");
            CreateIndex("dbo.Meals", "OrderItem_Id");
            CreateIndex("dbo.Meals", "OrderItem_Id1");
            AddForeignKey("dbo.Meals", "OrderItem_Id", "dbo.OrderItems", "Id");
            AddForeignKey("dbo.Addons", "OrderMeal_Id", "dbo.OrderMeals", "Id");
            AddForeignKey("dbo.Meals", "OrderItem_Id1", "dbo.OrderItems", "Id");
            DropColumn("dbo.Orders", "Name");
            DropColumn("dbo.Orders", "MealId");
            DropColumn("dbo.Orders", "AddonId");
            DropColumn("dbo.Orders", "DrinkId");
            DropColumn("dbo.Orders", "OrderDate");
            DropColumn("dbo.Orders", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "DrinkId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "AddonId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "MealId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Name", c => c.String(nullable: false));
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Meals", "OrderItem_Id1", "dbo.OrderItems");
            DropForeignKey("dbo.OrderMeals", "OrderItem_Id", "dbo.OrderItems");
            DropForeignKey("dbo.Addons", "OrderMeal_Id", "dbo.OrderMeals");
            DropForeignKey("dbo.Meals", "OrderItem_Id", "dbo.OrderItems");
            DropIndex("dbo.OrderMeals", new[] { "OrderItem_Id" });
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            DropIndex("dbo.Meals", new[] { "OrderItem_Id1" });
            DropIndex("dbo.Meals", new[] { "OrderItem_Id" });
            DropIndex("dbo.Addons", new[] { "OrderMeal_Id" });
            DropColumn("dbo.Meals", "OrderItem_Id1");
            DropColumn("dbo.Meals", "OrderItem_Id");
            DropColumn("dbo.Addons", "OrderMeal_Id");
            DropTable("dbo.SubmitedOrders");
            DropTable("dbo.OrderMeals");
            DropTable("dbo.OrderItems");
            CreateIndex("dbo.Orders", "DrinkId");
            CreateIndex("dbo.Orders", "AddonId");
            CreateIndex("dbo.Orders", "MealId");
            AddForeignKey("dbo.Orders", "MealId", "dbo.Meals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "DrinkId", "dbo.Drinks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "AddonId", "dbo.Addons", "Id", cascadeDelete: true);
        }
    }
}
