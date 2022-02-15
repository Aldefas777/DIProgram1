using Microsoft.AspNetCore.Mvc;

namespace DIProgram1.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public IActionResult Index()
        {
            return View();
        }

    }
}