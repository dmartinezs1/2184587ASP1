using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _2184587ASP1.Models;

namespace _2184587ASP1.Controllers
{
    public class CompraController : Controller
    {
        private inventarioEntities db = new inventarioEntities();
        // GET: Compra
        public ActionResult Index()
        {
            return View(db.compra.ToList());
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities())
            {
                var findCompra = db.compra.Find(id);
                return View(findCompra);
            }

        }

        // GET: Compra/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compra compra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.compra.Add(compra);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult ListarUsuarios()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public ActionResult listarClientes ()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.cliente.ToList());
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    compra findCompra = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        
        // POST: Compra/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(compra editCompra)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    compra compra = db.compra.Find(editCompra.id);
                    compra.fecha = editCompra.fecha;
                    compra.total = editCompra.total;                   
                    compra.id_usuario = editCompra.id_usuario;
                    compra.id_cliente = editCompra.id_cliente;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        // GET: Compra/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var findCompra = db.compra.Find(id);
                    db.compra.Remove(findCompra);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }


        }

    }
}
