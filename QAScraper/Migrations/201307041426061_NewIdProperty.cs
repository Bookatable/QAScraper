namespace QAScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewIdProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Sites", new[] { "Name" });
            AddPrimaryKey("dbo.Sites", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Sites", new[] { "Id" });
            AddPrimaryKey("dbo.Sites", "Name");
            DropColumn("dbo.Sites", "Id");
        }
    }
}
