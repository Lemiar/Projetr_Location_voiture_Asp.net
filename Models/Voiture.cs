using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoyerVoiture.Models
{
    public class Voiture
    {
        [Key]
        public int id { get; set; }
        public int? id_proprietaire { get; set; }
        [ForeignKey("id_proprietaire")]
        public virtual Utilisateur utilisateur { get; set; }
        [Display(Name = "Fuel")]
        public string carburant { get; set; }
        [Display(Name = "Mileage")]
        public float kilometrage { get; set; }
        [Display(Name = "Price")]
        public float prix { get; set; }
        [Display(Name = "Model")]
        public int model { get; set; }
        [Display(Name = "Name")]
        public string nom { set; get; }
        [Display(Name = "Image")]
        public string image { set; get; }
        [Display(Name = "Color")]
        public string couleur { set; get; }
        [Display(Name = "Number of places")]
        public int nombrePlace { get; set; }
        [Display(Name = "Horsepower")]
        public int chevaux { get; set; }
        [Display(Name = "State")]
        public string etat { get; set; }
        public string special { get; set; }

    }
}