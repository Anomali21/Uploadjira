using Microsoft.AspNetCore.Mvc;

namespace NRE_Portal.WebApp.Controllers
{
    public class NewRenewablesEnergysController : Controller
    {
        public IActionResult Index()
        {
            return View("NewRenewablesEnergys");
        }
    }
}