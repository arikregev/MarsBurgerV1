namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBooleanToBranch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "AccessibleBranch", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "AccessibleBranch");
        }
    }
}
