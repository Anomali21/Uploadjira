using Microsoft.AspNetCore.Mvc;
using NRE_Portal.WebApp.Models;
using System.Diagnostics;


namespace NRE_Portal.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
