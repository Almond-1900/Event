using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Event.Controllers
{
    public class HomeController : Controller
    {
        private readonly DB db;
        public HomeController(DB db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ocreate()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
