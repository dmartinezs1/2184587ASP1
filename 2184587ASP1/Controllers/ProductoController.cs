using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2184587ASP1.Models;
using Rotativa;

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
        public ActionResult Delete(int id)
        {                   
            try
                {
                using (var db = new inventarioEntities())
                {
                    var findUser = db.producto.Find(id);
                    db.producto.Remove(findUser);
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
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto findProduct = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto editProduct)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto product = db.producto.Find(editProduct.id);
                    product.nombre = editProduct.nombre;
                    product.percio_unitario = editProduct.percio_unitario;
                    product.descripcion = editProduct.descripcion;
                    product.cantidad = editProduct.cantidad;
                    product.id_proveedor = editProduct.id_proveedor;

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
        public ActionResult Reporte()
        {
            var db = new inventarioEntities();
            
                var query = from tabProveedor in db.proveedor
                            join tabProducto in db.producto on tabProveedor.id equals tabProducto.id_proveedor
                            select new Reporte
                            {
                                nombreProveedor = tabProveedor.nombre,
                                telefonoProveedor = tabProveedor.telefono,
                                direccionProveedor = tabProveedor.direccion,
                                nombreProducto = tabProducto.nombre,
                                precioProducto = tabProducto.percio_unitario
                            };
                return View(query);           
        }

        public ActionResult imprimirReporte()
        {
            return new ActionAsPdf("Reporte") { FileName = "Reporte.pdf" };
        }
    }
}