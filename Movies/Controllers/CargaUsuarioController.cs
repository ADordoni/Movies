using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;

namespace Movies.Controllers
{
    public class CargaUsuarioController : Controller
    {
        public ActionResult Index()
        {
            AdminUser mant = new AdminUser();
            return View(mant.LeerTodo());
        }
        public ActionResult Alta()
        {
            if (TempData.ContainsKey("error"))
            {
                ViewBag.mensaje = TempData["error"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Alta(IFormCollection dato)
        {
            AdminUser mant = new AdminUser();
            Usuario user = new Usuario();
            string confirm = dato["nombre"];

            if (confirm == "")
            {
                ViewBag.mensaje = "Error: debe ingresar un nombre";
                return View("Alta");
            }

            user = mant.LeerName(dato["nombre"]);
            DateTime fecha = new DateTime();
            bool verify = DateTime.TryParse(dato["fechanac"], out fecha);

            if (user.nombre != null)
            {
                ViewBag.mensaje = "Error: nombre ya ingresado";
                return View("Alta");
            }
            if (!verify)
            {
                ViewBag.mensaje = "Error: debe ingresar una fecha";
                return View("Alta");
            }
            if (dato["genero"] != "F" && dato["genero"] != "M" && dato["genero"] != "X")
            {
                ViewBag.mensaje = "Error: debe ingresar M (masculino) o F (femenino)";
                return View("Alta");
            }
            else
            {
                user.nombre = dato["nombre"];
                user.fecha = fecha;
                user.genero = dato["genero"];
                mant.Carga(user);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Modificacion(int id)
        {
            AdminUser mant = new AdminUser();
            Usuario user = new Usuario();
            user = mant.Leer(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Modificacion(IFormCollection dato)
        {
            AdminUser mant = new AdminUser();
            Usuario exist = new Usuario();
            Usuario user = new Usuario();
            string confirm = dato["nombre"];
            exist = mant.LeerName(dato["nombre"]);
            DateTime fecha = new DateTime();
            bool verify = DateTime.TryParse(dato["fechanac"], out fecha);

            user.id = int.Parse(dato["id"]);
            user.nombre = dato["nombre"];
            user.fecha = fecha;
            user.genero = dato["genero"];

            if (confirm == "")
            {
                ViewBag.mensaje = "Error: debe agregar un nombre de usuario";
                return View("Modificacion", user);
            }
            if (exist.nombre == user.nombre && exist.id != user.id)
            {
                ViewBag.mensaje = "Error: ese usuario ya está registrado con otro id";
                return View("Modificacion", user);
            }
            if (!verify)
            {
                 ViewBag.mensaje = "Error: debe ingresar una fecha válida";
                 return View("Modificacion", user);
            }
            if (dato["genero"] != "F" && dato["genero"] != "M")
            {
                ViewBag.mensaje = "Error: debe ingresar M (masculino) o F (femenino)";
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
