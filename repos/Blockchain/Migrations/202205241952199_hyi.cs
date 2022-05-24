namespace Blockchain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hyi : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Blocks", "FileType");
            AddColumn("dbo.Blocks", "Data_FileType", c => c.String());
        }

        public override void Down()
        {
        }
    }
}
