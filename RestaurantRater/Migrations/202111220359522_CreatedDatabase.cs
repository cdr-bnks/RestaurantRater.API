namespace RestaurantRater.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restaurants",
                r => new
                {
                    Id = r.Int(nullable: false, identity: true),
                    Name = r.String(nullable: false),
                    Address = r.String(nullable: false),
                    Rating = r.Double(nullable: false),
                })
                .PrimaryKey(p => p.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Restaurants");
        }
    }
}
