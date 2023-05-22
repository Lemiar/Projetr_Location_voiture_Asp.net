using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocationVoiture.Models;
using LoyerVoiture.Models;

namespace LocationVoiture.Controllers
{
    public class AdminController : Controller
    {
        public int idVoiture;

        private LoyerVoitureContext db = new LoyerVoitureContext();

        // GET: Admin
        public ActionResult accueil()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                return View(db.Voitures.ToList());
            }
            else return RedirectToAction("Index", "Home");
        }
        public ActionResult DetailsVoitures(int id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                idVoiture = id;
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Voiture utilisateur = db.Voitures.Find(id);
                if (utilisateur == null)
                {
                    return HttpNotFound();
                }

                return View(utilisateur);
            }
            else return RedirectToAction("Index", "Home");
        }
        public ActionResult Index()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {


                return View(db.Utilisateurs.ToList());
            }
            else return RedirectToAction("Index", "Home");
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Utilisateur utilisateur = db.Utilisateurs.Find(id);
                if (utilisateur == null)
                {
                    return HttpNotFound();
                }
                return View(utilisateur);
            }
            else return RedirectToAction("Index", "Home");
        }

        // GET: Admin/Create

        public ActionResult Create()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                return View("Create");
            }
            else return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Utilisateur user)
        {
            if (ModelState.IsValid)
            {
                if (db.Utilisateurs.Any(x => x.email == user.email) || db.Utilisateurs.Any(x => x.CIN == user.CIN))
                {
                    ViewBag.Notification = "Ce compte existe déjà!";
                    return View("Create");
                }
                else
                {
                    db.Utilisateurs.Add(user);
                    db.SaveChanges();

                    Session["id"] = user.id.ToString();
                    Session["email"] = user.email.ToString();
                    Session["mdp"] = user.mdp.ToString();

                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                return View("Create");
            }
        }

        // POST: Admin/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.

        /* public ActionResult Create([Bind(Include = "id,nom,prenom,email,mdp,remdp,ville,Adresse,CIN,Tel,statut")] Utilisateur utilisateur)
         {
             if (ModelState.IsValid)
             {
                 db.Utilisateurs.Add(utilisateur);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }

             return View(utilisateur);
         }*/

        // GET: Admin/Edit/5

        public ActionResult Edit(int? id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Utilisateur utilisateur = db.Utilisateurs.Find(id);
                if (utilisateur == null)
                {
                    return HttpNotFound();
                }
                return View(utilisateur);
            }
  
            else return RedirectToAction("Index", "Home");
    }

        // POST: Admin/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,prenom,email,mdp,remdp,ville,Adresse,CIN,Tel,statut")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilisateur);
        }
        public ActionResult EditV(int? id)
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

        // POST: Voitures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditV(Voiture voiture, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/images"), upload.FileName);
                upload.SaveAs(path);
                voiture.image = upload.FileName;
                voiture.id_proprietaire = int.Parse(Session["id"].ToString());

                db.Entry(voiture).State = EntityState.Modified;


                db.SaveChanges();
                return RedirectToAction("accueil");

            }
            return View(voiture);
        }


        public ActionResult DeleteV(int? id)
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

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("DeleteV")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedV(int id)
        {
            Voiture voiture = db.Voitures.Find(id);
            db.Voitures.Remove(voiture);
            db.SaveChanges();
            return RedirectToAction("accueil");
        }





        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var voiture = db.Voitures.Where(c => c.id_proprietaire == id).ToList();
            var prob=db.ProblemDeclarations.Where(c => c.id_locataire == id).ToList();

            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            foreach (var v in voiture)
            {
                db.Voitures.Remove(v);
            }

            foreach (var p in prob)
            {
                db.ProblemDeclarations.Remove(p);
            }
            db.Utilisateurs.Remove(utilisateur);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListeClientProb()
        {


            return View(db.ProblemDeclarations.ToList());

        }
        public ActionResult ListeClients()
        {


            return View(db.Reservations.ToList());

        }

        public ActionResult ListeNoir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur u = db.Utilisateurs.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
        [HttpPost, ActionName("ListeNoir")]
        [ValidateAntiForgeryToken]
        public ActionResult ListeNoirConfirmed(int id)
        {
            //id_proprietaire = int.Parse(Session["id"].ToString());

            foreach (var u in db.Utilisateurs)
            {
                if (u.id == id)
                {
                    if (u.Etat != "ListeNoir")
                    {
                        u.Etat = "ListeNoir";
                    }

                }
            }

            db.SaveChanges();

            return RedirectToAction("ListeNoirClient");
        }
        public ActionResult ListeNoirClient()
        {
            return View(db.Utilisateurs.Where(x => x.Etat.Equals("ListeNoir")).ToList());
        }
        public ActionResult DeleteListeNoir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur u = db.Utilisateurs.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
        [HttpPost, ActionName("DeleteListeNoir")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteListeNoirConfirmed(int id)
        {
            //id_proprietaire = int.Parse(Session["id"].ToString());

            foreach (var u in db.Utilisateurs)
            {
                if (u.id == id)
                {
                    u.Etat = null;
                }
            }


            db.SaveChanges();
            return RedirectToAction("ListeNoirClient");
        }
        ////LISTE Favoris//////
        public ActionResult ListeFav(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur u = db.Utilisateurs.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
        [HttpPost, ActionName("ListeFav")]
        [ValidateAntiForgeryToken]
        public ActionResult ListeFavConfirmed(int id)
        {

            foreach (var u in db.Utilisateurs)
            {

                if (u.id == id)
                {
                    if (u.Etat != "ListeFav")
                    {
                        u.Etat = "ListeFav";
                    }
                }
            }

            db.SaveChanges();


            return RedirectToAction("ListeFavClient");
        }
        public ActionResult ListeFavClient()
        {
            return View(db.Utilisateurs.Where(x => x.Etat.Equals("ListeFav")).ToList());
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult DeleteFav(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur u = db.Utilisateurs.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }
        [HttpPost, ActionName("DeleteFav")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFavConfirmed(int id)
        {
            //id_proprietaire = int.Parse(Session["id"].ToString());

            foreach (var u in db.Utilisateurs)
            {
                if (u.id == id)
                {
                    u.Etat = null;
                }
            }


            db.SaveChanges();
            return RedirectToAction("ListeFavClient");
        }
    }
}
