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
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(response);
                _context.SaveChanges();

                return RedirectToAction("Index"); // Redirect to Quadrant view after adding task
            }
            ViewBag.Categories = _context.Categories; // Add this line to repopulate categories in case of validation error
            return View("Task", response); // If model state is not valid, return back to the form with validation errors
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
        public IActionResult Edit(Tasks updatedInfo)
        {
            if (ModelState.IsValid)
            {
                if (updatedInfo.Completed == "True")
                {
                    // Delete the task from the database
                    _context.Tasks.Remove(updatedInfo);
                    _context.SaveChanges();

                    return RedirectToAction("Index"); // Redirect to Index page after deleting task
                }
                else
                {
                    // Update the task
                    _context.Update(updatedInfo);
                    _context.SaveChanges();

                    return RedirectToAction("Index"); // Redirect to Index page after updating task
                }
            }

            // If model state is not valid, return back to the form with validation errors
            return View("Edit", updatedInfo);
        }


        [HttpGet]
        public IActionResult Delete(int id) // gets the record we're going to delete
        {
            var recordToDelete = _context.Tasks
                .Single(x => x.TaskId == id);

            return View(recordToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Tasks task) // actually deletes the task
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
