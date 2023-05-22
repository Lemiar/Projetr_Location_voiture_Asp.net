using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LoyerVoiture.Models
{
    public class LoyerVoitureContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public LoyerVoitureContext() : base("name=LoyerVoitureContext")
        {
        }

        public System.Data.Entity.DbSet<LoyerVoiture.Models.Utilisateur> Utilisateurs { get; set; }

        public System.Data.Entity.DbSet<LoyerVoiture.Models.Voiture> Voitures { get; set; }

        public System.Data.Entity.DbSet<LocationVoiture.Models.Reservation> Reservations { get; set; }

        public System.Data.Entity.DbSet<LoyerVoiture.Models.ProblemDeclaration> ProblemDeclarations { get; set; }
    }
}
