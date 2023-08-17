using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using System.Diagnostics;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        
        public ActionResult Gestion()
        {
            return View();        
        }      
                
    }
}