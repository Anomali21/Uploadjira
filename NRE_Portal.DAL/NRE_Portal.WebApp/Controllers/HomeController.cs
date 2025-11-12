using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace NRE_Portal.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
