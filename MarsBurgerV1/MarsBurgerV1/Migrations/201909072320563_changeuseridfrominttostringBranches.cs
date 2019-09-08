namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeuseridfrominttostringBranches : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Branches", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Branches", "UserId", c => c.Int(nullable: false));
        }
    }
}
