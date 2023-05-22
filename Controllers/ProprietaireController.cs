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
    public class ProprietaireController : Controller
    {
        private LoyerVoitureContext db = new LoyerVoitureContext();

        public int id_proprietaire;
        public int idVoiture;
        int idPro;
        // GET: Locataire
        public ActionResult Index()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {

                ViewBag.idProprietaire = Session["id"];
                idPro = int.Parse(Session["id"].ToString());
                Session["id_Proprietaire"] = id_proprietaire;
                return View(db.Voitures.Where(x => x.id_proprietaire == idPro).ToList());
            }

            else return RedirectToAction("Index", "Home");


        }

        // GET: Proprietaire/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {

                id = int.Parse(Session["id"].ToString());

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

            return View(utilisateur);}

            else return RedirectToAction("Index", "Home");
        }


        public ActionResult DetailsLocataire(int id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                idVoiture = id;
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

        public ActionResult CreateVoitures()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {

                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        // POST: Voitures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVoitures(Voiture voiture, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/images"), upload.FileName);
                upload.SaveAs(path);
                voiture.image = upload.FileName;
                voiture.etat = "disponible";
                voiture.id_proprietaire = int.Parse(Session["id"].ToString());
                db.Voitures.Add(voiture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voiture);
        }



        // GET: Proprietaire/Edit/5
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

        // POST: Proprietaire/Edit/5
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

        // GET: Proprietaire/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Proprietaire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
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

        public ActionResult EditV(int? id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
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
            else return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index");

            }
            return View(voiture);
        }
        // GET: Voitures/Delete/5
        public ActionResult DeleteV(int? id)
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
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
            else return RedirectToAction("Index", "Home");
        }

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("DeleteV")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedV(int id)
        {
            Voiture voiture = db.Voitures.Find(id);
            db.Voitures.Remove(voiture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult HistoriqueProprietaire1()
        {
            if (Session["id"] != null && Session["email"] != null && Session["mdp"] != null)
            {
                id_proprietaire = int.Parse(Session["id"].ToString());
                return View(db.Reservations.Where(x => x.voiture.id_proprietaire == id_proprietaire).ToList());
            }
            else return RedirectToAction("Index", "Home");
        }


        public ActionResult HistoriqueProprietaire(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation res = db.Reservations.Find(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("HistoriqueProprietaire")]
        [ValidateAntiForgeryToken]
        public ActionResult historiqueConfirmedV(int id)
        {
            id_proprietaire = int.Parse(Session["id"].ToString());

            Reservation r = db.Reservations.Find(id);
            Voiture voiture = db.Voitures.Find(r.id_voiture);

            r.date_retour = DateTime.Now;
            voiture.etat = "disponible";
            db.SaveChanges();
            return RedirectToAction("Index");
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
            id_proprietaire = int.Parse(Session["id"].ToString());

            var result = db.Voitures.Where(a => a.nom.Contains(recherche)
            || (a.couleur.Contains(recherche) || a.prix.ToString().Contains(recherche)
            || a.kilometrage.ToString().Contains(recherche) || a.model.ToString().Contains(recherche))
             && a.id_proprietaire == id_proprietaire).ToList();

            return View(result);
        }


    }
}
