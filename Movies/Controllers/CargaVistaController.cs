using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;

namespace Movies.Controllers
{
    public class CargaVistaController : Controller
    {
        public ActionResult Index()
        {
            AdminView mant = new AdminView();
            return View(mant.LeerTodo());
        }
        public ActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Alta(IFormCollection dato)
        {
            AdminView mant = new AdminView();
            Usuario user = new Usuario();
            int i;
            bool verify = int.TryParse(dato["id"], out i);
            if (!verify)
            {
                ViewBag.mensaje = "Error: debe agregar un número de usuario";
                return View("Alta");
            }
            user = mant.Confirmar(i);
            string confirm = user.nombre;
            if (confirm == null)
            {
                ViewBag.mensaje = "Error: Ese usuario no existe";
                return View("Alta");
            }
            confirm = dato["pelicula"];
            if (confirm == "")
            {
                ViewBag.mensaje = "Error: Debe ingresar el nombre de una película";
                return View("Alta");
            }
            DateTime fecha = new DateTime();
            verify = DateTime.TryParse(dato["periodo"], out fecha);
            if (!verify)
            {
                ViewBag.mensaje = "Error: Debe ingresar una fecha válida";
                return View("Alta");
            }
            else
            {
                user.id = int.Parse(dato["id"]);
                user.vista = dato["pelicula"];
                user.fecha = DateTime.Parse(dato["periodo"]);
                mant.Carga(user);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Baja(int id)
        {
            AdminView mant = new AdminView();
            mant.Borrar(id);
            return RedirectToAction("Index");
        }
        public ActionResult Modificacion(int id)
        {
            AdminView mant = new AdminView();
            Usuario user = new Usuario();
            user = mant.Leer(id);
            if (TempData.ContainsKey("error"))
            {
                ViewBag.mensaje = TempData["error"];
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Modificacion(IFormCollection form)
        {
            AdminView mant = new AdminView();
            Usuario user = new Usuario();
            string confirm = form["pelicula"];
            DateTime fecha = new DateTime();
            bool verify = DateTime.TryParse(form["periodo"], out fecha);

            user.id2 = int.Parse(form["id2"]);
            user.id = int.Parse(form["id"]);
            user.nombre = form["nombre"];
            user.vista = confirm;
            user.fecha = fecha;

            if (confirm == "")
            {
                ViewBag.mensaje = "Error: debe escribir el nombre de una película";
                return View("Modificacion", user);
            }
            if (!verify)
            {
                ViewBag.mensaje = "Error: debe ingresar una fecha válida";
                return View("Modificacion", user);
            }
            else
            {
                mant.Modificar(user);
                return RedirectToAction("Index");
            }
        }
    }
}
