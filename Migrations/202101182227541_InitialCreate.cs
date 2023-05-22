namespace LocationVoiture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nom = c.String(nullable: false),
                        prenom = c.String(nullable: false),
                        email = c.String(),
                        mdp = c.String(nullable: false),
                        remdp = c.String(nullable: false),
                        ville = c.String(nullable: false),
                        Adresse = c.String(nullable: false),
                        CIN = c.String(nullable: false),
                        Tel = c.String(nullable: false),
                        statut = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Utilisateurs");
        }
    }
}
