using COMP2139_CumulativeLabs.Data;
using COMP2139_CumulativeLabs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_CumulativeLabs.Controllers {
    public class ProjectsController : Controller {

        private readonly ApplicationDbContext _dbContext;
        public ProjectsController(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IActionResult Index() {
            return View(_dbContext.Projects.ToList());
        }

        public IActionResult Details(int id) {
            var project = _dbContext.Projects
                .FirstOrDefault(p => p.ProjectId == id);
            return View(project);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project) {
            if (ModelState.IsValid) {
                _dbContext.Add(project);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public IActionResult Edit(int id) {
            var project = _dbContext.Projects.Find(id);
            if (project == null) {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProjectId, Name, Description")] Project project) {
            if (ModelState.IsValid) {
                try {
                    _dbContext.Update(project);
                    _dbContext.SaveChanges();
                } catch(DbUpdateConcurrencyException) {
                    if (!ProjectExists(project.ProjectId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public IActionResult Delete(int id) {
            var project = _dbContext.Projects.Find(id);
            if (project == null) {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int ProjectId) {
            var project = _dbContext.Projects.Find(ProjectId);
            if (project != null) {
                _dbContext.Remove(project);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        private bool ProjectExists(int id) {
            return _dbContext.Projects.Any(p => p.ProjectId == id);
        }
    }
}
