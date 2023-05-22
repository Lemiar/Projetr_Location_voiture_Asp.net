using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoyerVoiture.Models;


namespace LocationVoiture.Controllers
{

    public class HomeController : Controller
    {
        private LoyerVoitureContext db = new LoyerVoitureContext();

        public ActionResult Index()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null&& Session["nom_loc"] != null&& Session["prenom_loc"] != null)
            {
                Session["id"] = null;
                Session["email"] = null;
                Session["mdp"] = null;
                Session["nom_loc"] = null;
                Session["prenom_loc"] = null;

            }
            ViewData["nb_admin"] = db.Utilisateurs.Where(x => x.statut.Equals("Admin")).Count();
            ViewData["nb_prop"] = db.Utilisateurs.Where(x => x.statut.Equals("proprietaire")).Count();
            ViewData["nb_locataire"] = db.Utilisateurs.Where(x => x.statut.Equals("locataire")).Count();
            ViewData["nb_voiture"] = db.Voitures.Count();
            ViewData["nb_reservation"] = db.Voitures.Where(x=>x.etat.Equals("reserve")).Count();

            return View(db.Voitures.Where(x=>x.special.Equals("yes")).ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        ///login
        public ActionResult Signup()
        {
            return View("Signup");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       public ActionResult Signup(Utilisateur user)
        {
            if (ModelState.IsValid)
            {
                if (db.Utilisateurs.Any(x => x.email == user.email)&& db.Utilisateurs.Any(x => x.CIN == user.CIN))
                {
                    ViewBag.Notification = "Ce compte existe déjà!";
                    return View("Signup");
                }
                else
                {
                    db.Utilisateurs.Add(user);
                    db.SaveChanges();

                    Session["id"] = user.id.ToString();
                    Session["email"] = user.email.ToString();
                    Session["mdp"] = user.mdp.ToString();
                    if (user.statut.Equals("locataire"))
                    {
                        Session["id"] = user.id.ToString();
                        Session["email"] = user.email.ToString();
                        Session["mdp"] = user.mdp.ToString();
                        Session["nom_loc"] = user.nom.ToString();
                        Session["prenom_loc"] = user.prenom.ToString();


                        return RedirectToAction("Index", "Locataire");

                    }
                    else if (user.statut.Equals("proprietaire"))
                    {
                        Session["id"] = user.id.ToString();
                        Session["email"] = user.email.ToString();
                        Session["mdp"] = user.mdp.ToString();
                        Session["nom"] = user.nom.ToString();
                        Session["prenom"] = user.prenom.ToString();


                        return RedirectToAction("Index", "Proprietaire");

                    }
                    else if (user.statut.Equals("admin"))
                    {
                        Session["id"] = user.id.ToString();
                        Session["email"] = user.email.ToString();
                        Session["mdp"] = user.mdp.ToString();
                        Session["nom"] = user.nom.ToString();
                        Session["prenom"] = user.prenom.ToString();


                        return RedirectToAction("accueil", "Admin");

                    }
                    else
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View("Signup");
            }
        }
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Utilisateur u)
        {
            var checkLogin = db.Utilisateurs.Where(x => x.email.Equals(u.email) && x.mdp.Equals(u.mdp)).FirstOrDefault();
            if (checkLogin != null)
            {
                if(checkLogin.statut.Equals("locataire"))
                {
                    Session["id"] = checkLogin.id.ToString();
                    Session["email"] = checkLogin.email.ToString();
                    Session["mdp"] = checkLogin.mdp.ToString();
                    Session["nom_loc"] = checkLogin.nom.ToString();
                    Session["prenom_loc"] = checkLogin.prenom.ToString();


                    return RedirectToAction("Index", "Locataire");

                }
                else if (checkLogin.statut.Equals("proprietaire"))
                {
                    Session["id"] = checkLogin.id.ToString();
                    Session["email"] = checkLogin.email.ToString();
                    Session["mdp"] = checkLogin.mdp.ToString();
                    Session["nom"] = checkLogin.nom.ToString();
                    Session["prenom"] = checkLogin.prenom.ToString();


                    return RedirectToAction("Index", "Proprietaire");

                }
                else if (checkLogin.statut.Equals("admin"))
                {
                    Session["id"] = checkLogin.id.ToString();
                    Session["email"] = checkLogin.email.ToString();
                    Session["mdp"] = checkLogin.mdp.ToString();
                    Session["nom"] = checkLogin.nom.ToString();
                    Session["prenom"] = checkLogin.prenom.ToString();


                    return RedirectToAction("accueil", "Admin");

                }

            }
            else
            {
                ViewBag.Notification = "Email ou mot de passe incorrect";

            }
            return View();
        }

       
        public ActionResult Rechercher()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Rechercher(string recherche)
        {
            var result = db.Voitures.Where(a => (a.nom.Contains(recherche)
            || a.utilisateur.nom.ToString().Contains(recherche)
             || a.utilisateur.prenom.ToString().Contains(recherche)
            || a.couleur.Contains(recherche) || a.prix.ToString().Contains(recherche)
            || a.kilometrage.ToString().Contains(recherche) || a.model.ToString().Contains(recherche)) && a.special.Equals("yes")).ToList();

            return View(result);
        }
    }
}