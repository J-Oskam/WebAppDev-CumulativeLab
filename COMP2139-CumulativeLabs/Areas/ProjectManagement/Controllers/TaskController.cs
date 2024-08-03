using COMP2139_CumulativeLabs.Areas.ProjectManagement.Models;
using COMP2139_CumulativeLabs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_CumulativeLabs.Areas.ProjectManagement.Controllers
{

    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("Index/{projectId:int}")]
        public async Task<IActionResult> Index(int projectId)
        {
            var tasks = await _dbContext.ProjectTasks
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();

            ViewBag.ProjectId = projectId;
            return View(tasks);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        { //.include means give access to otherside of the entity, that being the project navigation property
            var task = await _dbContext.ProjectTasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.ProjectTaskId == id); //will return the first value found or null

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpGet("Create/{projectId:int}")]
        public async Task<IActionResult> Create(int projectId)
        {
            var project = await _dbContext.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var task = new ProjectTask
            {
                ProjectId = projectId
            };

            return View(task);
        }

        [HttpPost("Create/{projectId:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.ProjectTasks.AddAsync(task);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", new { task.ProjectId });
            }

            var projects = await _dbContext.Projects.ToListAsync();

            ViewBag.Projects = new SelectList(projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _dbContext.ProjectTasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.ProjectTaskId == id);

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost("DeleteConfirmed/{projectTaskId:int}")]
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int projectTaskId)
        {
            var task = await _dbContext.ProjectTasks.FindAsync(projectTaskId);
            if (task != null)
            {
                _dbContext.ProjectTasks.Remove(task);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", new { task.ProjectId });
            }
            return NotFound();
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _dbContext.ProjectTasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.ProjectTaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            var projects = await _dbContext.Projects.ToListAsync();

            ViewBag.Projects = new SelectList(projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (id != task.ProjectTaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.ProjectTasks.Update(task);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", new { projectId = task.ProjectId });
            }

            var projects = await _dbContext.Projects.ToListAsync();

            ViewBag.Projects = new SelectList(projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet("Search/{projectId:int}/{searchString?}")]
        public async Task<IActionResult> Search(int projectId, string searchString)
        {
            var tasksQuery = _dbContext.ProjectTasks.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                tasksQuery = tasksQuery.Where(t => t.Title.Contains(searchString)
                                            || t.Description.Contains(searchString));
            }

            var tasks = await tasksQuery.ToListAsync();
            ViewBag.ProjectId = projectId; //keeps track of current project
            return View("Index", tasks);
        }
    }
}