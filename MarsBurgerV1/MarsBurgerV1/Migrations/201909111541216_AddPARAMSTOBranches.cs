namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPARAMSTOBranches : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "hasParking", c => c.Boolean(nullable: false));
            AddColumn("dbo.Branches", "Kosher", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "Kosher");
            DropColumn("dbo.Branches", "hasParking");
        }
    }
}
