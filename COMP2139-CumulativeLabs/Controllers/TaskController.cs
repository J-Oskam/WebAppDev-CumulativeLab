using COMP2139_CumulativeLabs.Data;
using COMP2139_CumulativeLabs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_CumulativeLabs.Controllers {
    public class TaskController : Controller {
        private readonly ApplicationDbContext _dbContext;

        public TaskController(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IActionResult Index(int projectId) {
            var tasks = _dbContext.ProjectTasks
                .Where(pt => pt.ProjectId == projectId)
                .ToList();

            ViewBag.ProjectId = projectId;
            return View(tasks);
        }

        public IActionResult Details(int id) { //.include means give access to otherside of the entity, that being the project navigation property
            var task = _dbContext.ProjectTasks
                .Include(pt => pt.Project)
                .FirstOrDefault(pt => pt.ProjectId == id); //will return the first value found or null

            if(task == null) {
                return NotFound();
            }
            return View(task);
        }

        public IActionResult Create(int projectId) {
            var project = _dbContext.Projects.Find(projectId);
            if(project == null) {
                return NotFound();
            }

            var task = new ProjectTask {
                ProjectId = projectId
            };

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title", "Description", "ProjectId")] ProjectTask task) {
            if (ModelState.IsValid) {
                _dbContext.ProjectTasks.Add(task);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", new { ProjectId = task.ProjectId });
            }

            ViewBag.Projects = new SelectList(_dbContext.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        public IActionResult Delete(int id) {
            var task = _dbContext.ProjectTasks
                .Include(pt => pt.Project)
                .FirstOrDefault(pt => pt.ProjectTaskId == id);

            if (task == null) {
                return NotFound();
            }
            //I don't think this viewbag is used at all in the delete get method
            //ViewBag.Projects = new SelectList(_dbContext.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int projectTaskId) {
            var task = _dbContext.ProjectTasks.Find(projectTaskId);
            if(task != null) {
                _dbContext.Remove(task);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", new { ProjectId = task.ProjectId });
            }
            return NotFound();
        }

        public IActionResult Edit(int id) {
            var task = _dbContext.ProjectTasks
                .Include(pt => pt.Project)
                .FirstOrDefault(pt => pt.ProjectTaskId == id);

            if(task == null) {
                return NotFound();
            }

            ViewBag.Projects = new SelectList(_dbContext.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind ("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask task) {
            if(id != task.ProjectTaskId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                _dbContext.ProjectTasks.Update(task);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", new { projectId = task.ProjectId });
            }

            ViewBag.Projects = new SelectList(_dbContext.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }
    }
}