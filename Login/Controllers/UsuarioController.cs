using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;



namespace Login.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            //return View();
            using (MiDbContext db = new MiDbContext())
            {
                return View(db.usuario.ToList());
            }
        }

        public ActionResult Registrar()
        {
            return View();
        }
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register_Practica(Register_PracticaViewModel model)
        public ActionResult Registrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                using (MiDbContext db = new MiDbContext())
                {
                    db.usuario.Add(usuario);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = usuario.Nombre + " Registrado correctamente.";

                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario usuario)
        {
            using (MiDbContext db = new MiDbContext())
            {
                Usuario U = null;
                try {
                    U = db.usuario.Single(u => u.Nombre == usuario.Nombre && u.Password == usuario.Password);
                }catch(Exception Ex)
                {
                    U = null;
                }
                
                if (U != null)
                {
                    Session["Id"] = usuario.Id.ToString();
                    Session["Nombre"] = usuario.Nombre.ToString();
                    //return RedirectToAction("LoggedIn");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Uusario o password incorrectos.");
                }
            }
            return View();
        }


        public ActionResult LoggedIn()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //Session.Remove(Session["Nombre"].ToString());
            Session["Id"] = null;
            Session["Nombre"] = null;
            return RedirectToAction("Index", "Usuario");
        }


        public ActionResult BorrarDatosDePrueba()
        {            
            using (MiDbContext db = new MiDbContext())
            {                
                foreach(Usuario u in db.usuario.ToList())
                {
                    db.usuario.Remove(u);
                }                
                db.SaveChanges();
                
            }
            ModelState.Clear();
            ViewBag.Message = " Eliminado todo.";

            return RedirectToAction("Index");
        }

    }
}