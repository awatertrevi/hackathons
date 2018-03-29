namespace FoundIt.Webservice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LostItems", "Latitude", c => c.Double());
            AlterColumn("dbo.LostItems", "Longitude", c => c.Double());
            AlterColumn("dbo.LostItems", "Radius", c => c.Double());
            AlterColumn("dbo.LostItems", "IsStolen", c => c.Boolean());
            AlterColumn("dbo.LostItems", "EstimatedLoseTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LostItems", "EstimatedLoseTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LostItems", "IsStolen", c => c.Boolean(nullable: false));
            AlterColumn("dbo.LostItems", "Radius", c => c.Double(nullable: false));
            AlterColumn("dbo.LostItems", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.LostItems", "Latitude", c => c.Double(nullable: false));
        }
    }
}
