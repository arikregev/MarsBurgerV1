namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Drinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        MealId = c.Int(nullable: false),
                        AddonId = c.Int(nullable: false),
                        DrinkId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addons", t => t.AddonId, cascadeDelete: true)
                .ForeignKey("dbo.Drinks", t => t.DrinkId, cascadeDelete: true)
                .ForeignKey("dbo.Meals", t => t.MealId, cascadeDelete: true)
                .Index(t => t.MealId)
                .Index(t => t.AddonId)
                .Index(t => t.DrinkId);
            
            CreateTable(
                "dbo.SideDishes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "MealId", "dbo.Meals");
            DropForeignKey("dbo.Orders", "DrinkId", "dbo.Drinks");
            DropForeignKey("dbo.Orders", "AddonId", "dbo.Addons");
            DropIndex("dbo.Orders", new[] { "DrinkId" });
            DropIndex("dbo.Orders", new[] { "AddonId" });
            DropIndex("dbo.Orders", new[] { "MealId" });
            DropTable("dbo.SideDishes");
            DropTable("dbo.Orders");
            DropTable("dbo.Meals");
            DropTable("dbo.Drinks");
            DropTable("dbo.Addons");
        }
    }
}
