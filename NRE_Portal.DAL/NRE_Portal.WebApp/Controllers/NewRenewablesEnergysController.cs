using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NRE_Portal.WebApp.Controllers
{
    public class NewRenewablesEnergysController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View("NewRenewablesEnergys");
        }
    }
}