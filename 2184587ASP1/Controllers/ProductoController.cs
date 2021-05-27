﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2184587ASP1.Models;

namespace _2184587ASP1.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto.ToList());
            }
            
        }

        public static string NombreProveedor(int? idProveedor)
        {
            using (var db = new inventarioEntities())

            {
                return db.proveedor.Find(idProveedor).nombre;

            }
        }

        public ActionResult ListarProveedores()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.proveedor.ToList());
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities())
            {
                var findProduct = db.producto.Find(id);
                return View(findProduct);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto producto)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities())
                {
                    db.producto.Add(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

    }
}