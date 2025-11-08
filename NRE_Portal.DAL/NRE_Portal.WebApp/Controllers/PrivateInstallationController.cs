using Microsoft.AspNetCore.Mvc;

namespace NRE_Portal.WebApp.Controllers
{
    public class PrivateInstallationController : Controller
    {
        public IActionResult Index()
        {
            return View("PrivateInstallation");
        }

        [HttpGet]
        public IActionResult GetComponent(string section)
        {
            return section switch
            {
                "location" => PartialView("Components/_LocationComponent"),
                "type" => PartialView("Components/_TypeComponent"),
                "orientation" => PartialView("Components/_OrientationComponent"),
                "area" => PartialView("Components/_AreaComponent"),
                _ => PartialView("Components/_LocationComponent")
            };
        }
    }
}