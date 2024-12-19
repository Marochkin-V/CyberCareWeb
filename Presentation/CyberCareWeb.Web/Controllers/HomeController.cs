using Microsoft.AspNetCore.Mvc;

namespace CyberCareWeb.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
