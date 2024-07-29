using COMP2139_CumulativeLabs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace COMP2139_CumulativeLabs.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult About() {
            return View();
        }

        public IActionResult GeneralSearch(string searchType, string searchString) {
            if(searchType == "Projects") {
                //redirects to project search
                return RedirectToAction("Search", "Projects", new { searchString });
            } else if(searchType == "Tasks"){
                int defaultProjectId = 1;
                return RedirectToAction("Search", "Task", new { projectId = defaultProjectId, searchString = searchString });
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult NotFound(int statusCode) {
            if(statusCode == 404) {
                return View();
            }
            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
