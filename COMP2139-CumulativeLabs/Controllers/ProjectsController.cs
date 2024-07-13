using COMP2139_CumulativeLabs.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP2139_CumulativeLabs.Controllers {
    public class ProjectsController : Controller {
        [HttpGet]
        public IActionResult Index() {
            var projects = new List<Project>() {
                new Project { ProjectId = 1, Name = "Project 1", Description = "first project"}
            };
            return View(projects);
        }

        [HttpGet]
        public IActionResult Details(int id) {
            var project = new Project { ProjectId = id, Name = "Project " + id, Description = "Details of project " + id };
            return View(project);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Project project) {
            return RedirectToAction("Index");
        }

    }
}
