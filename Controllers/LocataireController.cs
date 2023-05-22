using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocationVoiture.Models;
using LoyerVoiture.Models;

namespace LocationVoiture.Controllers
{
    public class LocataireController : Controller
    {
        private LoyerVoitureContext db = new LoyerVoitureContext();

        public int id_locataire;
        public int idVoiture;
        // GET: Locataire
        public ActionResult Index()
        {
            if (Session["id"] != null&& Session["email"]!=null && Session["mdp"]!=null)
            {

                ViewBag.idlocataire = Session["id"];
                return View(db.Voitures.Where(x=>x.etat.Equals("disponible")).ToList());
            }

            else return RedirectToAction("Index","Home");


             }

        public ActionResult Special()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {

                ViewBag.idlocataire = Session["id"];
                return View(db.Voitures.Where(x => x.special.Equals("yes") & x.etat.Equals("disponible")).ToList());

            }
            else return RedirectToAction("Index", "Home");


        }

        // GET: Locataire/Details/5
        public ActionResult DetailsLocataire(int? id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {

                id = int.Parse(Session["id"].ToString());
                Utilisateur user = db.Utilisateurs.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else return RedirectToAction("Index", "Home");

        }
        public ActionResult DetailsVoitures(int id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                idVoiture = id;
                Session["id_voiture"] = id;
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Voiture v = db.Voitures.Find(id);
                if (v == null)
                {
                    return HttpNotFound();
                }

                return View(v);
            }
        
            else return RedirectToAction("Index", "Home");

        }


        // POST: Locataire/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
       
        public ActionResult Edit(int? id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                return View(db.Utilisateurs.Find(id));
            }

            else return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,prenom,email,mdp,remdp,ville,Adresse,CIN,Tel,statut")] Utilisateur utilisateur)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {

                if (ModelState.IsValid)
            {
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Locataire");
            }

              else return RedirectToAction("Index", "Home");
        }

    
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
       
        
        public ActionResult Reservation(int id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                idVoiture = id;
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Voiture v = db.Voitures.Find(id);
                if (v == null)
                {
                    return HttpNotFound();
                }

                return View(v);
            }
            else return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservation([Bind(Include = "idReservation,id_proprietaire,id_voiture,date_reservation,date_retour,TypePaiment")] Reservation reservation)
        {
            
            id_locataire = int.Parse(Session["id"].ToString());



            if (ModelState.IsValid)
            {
              
           
                
                    reservation.id_proprietaire = id_locataire;
                    reservation.id_voiture = int.Parse(Session["id_voiture"].ToString());
                Voiture v = db.Voitures.Find(reservation.id_voiture);
                v.etat = "reserve";
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return RedirectToAction("HistoriqueLocataire");
                


            }

            return View(reservation);

        }

        public ActionResult HistoriqueLocataire()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                id_locataire = int.Parse(Session["id"].ToString());

            return View(db.Reservations.Where(x => x.id_proprietaire == id_locataire).ToList()); }
            
            
            else return RedirectToAction("Index", "Home");

        }

   
        public ActionResult Rechercher()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");

        }
        [HttpPost]

        public ActionResult Rechercher(string recherche)
        {
            var result = db.Voitures.Where(a => (a.nom.Contains(recherche)
            || a.utilisateur.nom.ToString().Contains(recherche)
             || a.utilisateur.prenom.ToString().Contains(recherche)
            || a.couleur.Contains(recherche) || a.prix.ToString().Contains(recherche)
            || a.kilometrage.ToString().Contains(recherche) || a.model.ToString().Contains(recherche)) & a.etat.Equals("disponible") ).ToList();

            return View(result);
        }


        public ActionResult RechercherDate(DateTime date)
        {
            ViewBag.idlocataire = Session["id"];
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                var list0 = db.Reservations.Where(x => x.date_retour < date & x.voiture.etat.Equals("reserve")).ToList();
                var List = list0.ToArray();

                List<int?> Id = new List<int?>();
                
                foreach (var item in List)
                {
                    if (!Id.Contains(item.id_voiture))
                        Id.Add(item.id_voiture);
                }
                    
                var listV = db.Voitures.Where(x => x.etat.Equals("disponible")).ToList();

                foreach (var item in Id)
                {
                    Voiture v = db.Voitures.Find(item);
                    listV.Add(v);
                }

                return View(listV);
            }
            else return RedirectToAction("Index", "Home");
        }


        public ActionResult Problem()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        // POST: Locataire/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Problem( ProblemDeclaration p)
        {
            id_locataire = int.Parse(Session["id"].ToString());

            if (ModelState.IsValid)
            {
                p.id_locataire = id_locataire;
                db.ProblemDeclarations.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(p);
        }



    }
}
