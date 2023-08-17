using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;

namespace Movies.Controllers
{
    public class VistasController : Controller
    {        
        public ActionResult Index()
        {
            Visualizacion mant = new Visualizacion();
            return View(mant.LeerTodo());
        }
        public ActionResult Periodo()
        {
            Visualizacion mant = new Visualizacion();
            return View(mant.CountPeriod());
        }
        public ActionResult Edad()
        {
            Visualizacion mant = new Visualizacion();
            return View(mant.CountAge());
        }
        public ActionResult Sexo()
        {
            Visualizacion mant = new Visualizacion();
            return View(mant.CountGenre());
        }
        public ActionResult PeriodoSexo()
        {
            Visualizacion mant = new Visualizacion();
            return View(mant.CountGenrePeriod());
        }
    }
}
