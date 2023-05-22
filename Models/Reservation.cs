using LoyerVoiture.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace LocationVoiture.Models
{
    public class Reservation
    {
      [Key]
      public int idReservation { get; set; }

        public int? id_proprietaire { get; set; }
        [ForeignKey("id_proprietaire")]
        public virtual Utilisateur utilisateur { get; set; }

        public int? id_voiture { get; set; }
        [ForeignKey("id_voiture")]
        public virtual Voiture voiture { get; set; }

        public DateTime date_reservation { get; set; }

        public DateTime date_retour { get; set; }

        public string TypePaiment { get; set; }


    }
}