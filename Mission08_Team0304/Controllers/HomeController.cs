using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var tasks = _context.Tasks.Include(t => t.Category).ToList();
            if (tasks != null && tasks.Any()) // Check if tasks is not null and not empty
            {
                return View("Quadrant", tasks);
            }
            else
            {
                // If tasks is null or empty, handle the error gracefully
                return View("Quadrant"); // Example: return an error view
            }
        }


        [HttpGet]
        public IActionResult Task()
        {
            ViewBag.Categories = _context.Categories;

            return View("Task", new Tasks());
        }

        [HttpPost]
        public IActionResult Task(Tasks response)
        {
            _context.Tasks.Add(response);
            _context.SaveChanges();

            return View("Quadrant");
        }

        [HttpGet]
        public IActionResult Edit(int id) // this makes our edit button returns the right record
        {
            var recordToEdit = _context.Tasks
                .Single(x => x.TaskId == id);

            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.Name)
                .ToList();
            return View("Task", recordToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Task updatedInfo) // this makes sure that our edits are saved
        {
            _context.Update(updatedInfo);
            _context.SaveChanges();

            return RedirectToAction("Index"); // this makes sure the quadrant view is properly shown
        }

        [HttpGet]
        public IActionResult Delete(int id) // gets the record we're going to delete
        {
            var recordToDelete = _context.Tasks
                .Single(x => x.TaskId == id);

            return View(recordToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Tasks task) // actually deletes the movie
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // other controllers necessary

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
