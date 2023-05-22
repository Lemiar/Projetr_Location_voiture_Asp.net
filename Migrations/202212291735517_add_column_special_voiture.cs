namespace LocationVoiture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column_special_voiture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voitures", "special", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Voitures", "special");
        }
    }
}
