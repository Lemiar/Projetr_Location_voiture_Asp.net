using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoyerVoiture.Models
{
    public class Utilisateur
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Required fields", AllowEmptyStrings = false)]

        [Display(Name = "Last name")]
        public string nom { get; set; }
        [Required(ErrorMessage = "Required fields", AllowEmptyStrings = false)]

        [Display(Name = "First name")]
        public string prenom { get; set; }

        [Required(ErrorMessage = "Required fields", AllowEmptyStrings = false)]

        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        [Display(Name = "Email")]

        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        public string mdp { get; set; }
        [Required]
        [Compare("mdp")]
        [DataType(DataType.Password)]
        [Display(Name = "Reenter password")]
        public string remdp { get; set; }
        [Required]
        [Display(Name = "City")]
        public string ville { set; get; }
        [Required]
        [Display(Name = "Address")]
        public string Adresse { set; get; }
        [Required]
        public string CIN { set; get; }
        [Required]
        [Display(Name = "Phone")]
        public string Tel { get; set; }
        [Required(ErrorMessage ="State required")]
        [Display(Name = "Status")]
        public string statut { get; set; }
        [Display(Name = "State")]
        public string Etat { get; set; }
    }
}