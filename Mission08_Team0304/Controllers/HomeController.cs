using Microsoft.AspNetCore.Mvc;
using Mission08_Team0304.Models;
using System.Diagnostics;

namespace Mission08_Team0304.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TasksContext _context;

        public HomeController(ILogger<HomeController> logger, TasksContext temp)
        {
            _context = temp;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Quadrant");
        }

        public IActionResult Task()
        {
            return View();
        }


        // other controllers necessary

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
