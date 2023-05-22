namespace LocationVoiture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FkAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voitures", "id_proprietaire", c => c.Int());
            CreateIndex("dbo.Voitures", "id_proprietaire");
            AddForeignKey("dbo.Voitures", "id_proprietaire", "dbo.Utilisateurs", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Voitures", "id_proprietaire", "dbo.Utilisateurs");
            DropIndex("dbo.Voitures", new[] { "id_proprietaire" });
            DropColumn("dbo.Voitures", "id_proprietaire");
        }
    }
}
