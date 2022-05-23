namespace Blockchain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data_Content = c.String(),
                        Data_Hash = c.String(),
                        Data_Type = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Hash = c.String(),
                        PrevHash = c.String(),
                        User_Login = c.String(),
                        User_Role = c.Int(nullable: false),
                        User_Password = c.String(),
                        User_Hash = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Blocks");
        }
    }
}
