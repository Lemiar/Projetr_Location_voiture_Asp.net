using LoyerVoiture.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoyerVoiture.Models
{
    public class ProblemDeclaration
    {
        [Key]
        public int id_prob { get; set; }

        public int? id_locataire { get; set; }
        [ForeignKey("id_locataire")]
        public virtual Utilisateur utilisateur { get; set; }
        public string probleme { get; set; }
    }
}