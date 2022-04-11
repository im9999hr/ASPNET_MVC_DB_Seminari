using SeminarUpisi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SeminarUpisi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        #region Predbiljezba
        [AllowAnonymous]
        public ActionResult Index(string searchString)
        {
            try
            {
                List<Seminar> sviSeminari = (from s in _db.Seminari orderby s.Naziv select s).ToList();
                List<Seminar> nepopunjeniSeminari = sviSeminari.Where(x => x.Popunjen == false).ToList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    nepopunjeniSeminari = nepopunjeniSeminari.Where(s => s.Naziv.Contains(searchString)).ToList();
                }
                return View(nepopunjeniSeminari);
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }

        }
        [AllowAnonymous]
        public ActionResult DodajPredbiljezbu(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Seminar seminar = _db.Seminari.Find(id);
                if (seminar == null)
                {
                    return HttpNotFound();
                }
                Predbiljezba p = new Predbiljezba();
                p.Seminar = seminar;
                return View(p);
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DodajPredbiljezbu(Predbiljezba p, int id)
        {
            try
            {
                Seminar seminar = _db.Seminari.Find(id);
                p.Seminar = seminar;
                p.Status = nameof(StatusPredbiljezbe.Neobradjena);
                p.Datum = DateTime.Now;
                if (ModelState.IsValid)
                {
                    seminar.Predbiljezbe.Add(p);
                    _db.SaveChanges();
                    TempData["Poruka"] = "Podaci uspješno pohranjeni!";
                    return RedirectToAction("Index");
                }
                return View(p);              
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }

        }

        #endregion

        #region Seminari
        public ActionResult Seminari(string searchString)
        {
            try
            {
                List<Seminar> sviSeminari = (from s in _db.Seminari orderby s.Naziv select s).ToList();
                foreach (Seminar seminar in sviSeminari)
                {
                    seminar.Predbiljezbe = (from pr in _db.Predbiljezbe where pr.IdSeminar == seminar.IdSeminar select pr).ToList();
                }
                if (!String.IsNullOrEmpty(searchString))
                {
                    sviSeminari = sviSeminari.Where(s => s.Naziv.Contains(searchString)).ToList();
                }
                return View(sviSeminari);
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }

        }

        public ActionResult UnesiSeminar()
        {
            Seminar noviSeminar = new Seminar() { Datum=DateTime.Now };
            return View(noviSeminar);
        }

        [HttpPost]
        public ActionResult UnesiSeminar(Seminar noviSeminar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Seminari.Add(noviSeminar);
                    _db.SaveChanges();
                    TempData["Poruka"] = "Seminar uspješno spremljen!";
                    return RedirectToAction("Seminari");
                }
                return View(noviSeminar);
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }
        }

        public ActionResult UrediSeminar(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Seminar seminar = _db.Seminari.Find(id);
                if (seminar == null)
                {
                    return HttpNotFound();
                }
                return View(seminar);
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult UrediSeminar(Seminar seminar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(seminar).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["Poruka"] = "Promjene o seminaru uspješno spremljene!";
                    return RedirectToAction("Seminari");
                }
                return View(seminar);
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }
        }

        public ActionResult IzbrisiSeminar(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Seminar seminar = _db.Seminari.Find(id);
                if (seminar == null)
                {
                    return HttpNotFound();
                }
                return View(seminar);
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }
        }
        [HttpPost, ActionName("IzbrisiSeminar")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                Seminar seminar = _db.Seminari.Find(id);
                _db.Seminari.Remove(seminar);
                _db.SaveChanges();
                TempData["Poruka"] = "Seminar obrisan!";
                return RedirectToAction("Seminari");
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }

        }
        #endregion

        #region Predbiljezbe
        public ActionResult Predbiljezbe(string predbiljezbeStatus, string searchString)
        {
            try
            {
                List<string> statusLst = new List<string>();
                List<string> statusQry = (from d in _db.Predbiljezbe orderby d.Status select d.Status).ToList();
                statusLst.AddRange(statusQry.Distinct());
                ViewBag.predbiljezbeStatus = new SelectList(statusLst);

                List<Predbiljezba> predbiljezbe = (from p in _db.Predbiljezbe orderby p.Seminar.Naziv select p).ToList();
                //List<Predbiljezba> predbiljezbe = _db.Predbiljezbe.ToList();
                foreach (Predbiljezba item in predbiljezbe)
                {
                    Seminar seminar = _db.Seminari.Find(item.IdSeminar);
                    item.Seminar = seminar;
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    predbiljezbe = predbiljezbe.Where(s => s.Seminar.Naziv.Contains(searchString)).ToList();
                }

                if (!string.IsNullOrEmpty(predbiljezbeStatus))
                {
                    predbiljezbe = predbiljezbe.Where(x => x.Status == predbiljezbeStatus).ToList();
                }

                return View(predbiljezbe);
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }
        }

        public ActionResult ObradiPredbiljezbu(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Predbiljezba predbiljezba = _db.Predbiljezbe.Find(id);
                if (predbiljezba == null)
                {
                    return HttpNotFound();
                }
                Seminar seminar = _db.Seminari.Find(predbiljezba.IdSeminar);
                if (seminar == null)
                {
                    return HttpNotFound();
                }
                //predbiljezba.Seminar = seminar;
                return View(predbiljezba);
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }

        }
        [HttpPost]
        public ActionResult ObradiPredbiljezbu(Predbiljezba predbiljezba)
        {
            try
            {
                // slučaj ako se promijeni i naziv seminara tokom obrade predbilježbe (ostavio sam tu opciju)
                string nazivSeminara = predbiljezba.Seminar.Naziv;
                Seminar seminar = _db.Seminari.Find(predbiljezba.IdSeminar);
                predbiljezba.Seminar = seminar;
                predbiljezba.Seminar.Naziv = nazivSeminara;
                // ovo je slučaj ako se snime podaci, a nije izabrano Odbijena ili Prihvaćena da se ne upiše prazan string
                if (string.IsNullOrEmpty(predbiljezba.Status))
                {
                    predbiljezba.Status = nameof(StatusPredbiljezbe.Neobradjena);
                }
                if (ModelState.IsValid)
                {
                    _db.Entry(predbiljezba).State = EntityState.Modified; //osim podataka za predbilježbu promjeni se naziv seminara (ukoliko se novi naziv upiše)
                    _db.SaveChanges();
                    TempData["Poruka"] = "Podaci uspješno pohranjeni!";
                    return RedirectToAction("Predbiljezbe");
                }
                return View(predbiljezba);
            }
            catch (Exception e)
            {
                ViewBag.Greska = "Dogodila se greška: " + e.Message;
                return View("Error");
            }
        }
        #endregion
    }
}