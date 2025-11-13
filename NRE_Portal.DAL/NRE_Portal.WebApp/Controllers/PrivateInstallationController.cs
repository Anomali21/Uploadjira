using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NRE_Portal.WebApp.Controllers
{
    public class PrivateInstallationController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View("PrivateInstallation");
        }

        [HttpGet]
        public async Task<IActionResult> GetComponent(string section)
        {
            return section switch
            {
                "location" => PartialView("Components/_LocationComponent"),
                "type" => PartialView("Components/_TypeComponent"),
                "orientation" => PartialView("Components/_OrientationComponent"),
                "area" => PartialView("Components/_AreaComponent"),
                _ => NotFound()
            };
        }

        [HttpPost]
        public IActionResult SaveData(string InstallationName, string Location, int Length, int Width, int Height)
        {

            Console.WriteLine($"nom de l'installatiion : {InstallationName}, location : {Location}, dimension : {Length}x{Width}x{Height}");

            return RedirectToAction("SaveScreen");
        }

    }
}