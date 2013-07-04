namespace QAScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 4000),
                        Url = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sites");
        }
    }
}
