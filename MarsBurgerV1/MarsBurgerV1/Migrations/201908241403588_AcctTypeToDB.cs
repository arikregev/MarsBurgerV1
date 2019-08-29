namespace MarsBurgerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcctTypeToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            //Sql("INSERT INTO [dbo].[AccountType](Name) VALUES('Admin')");
            //Sql("INSERT INTO [dbo].[AccountType](Name) VALUES('Customer')");
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountTypes");
        }
    }
}
