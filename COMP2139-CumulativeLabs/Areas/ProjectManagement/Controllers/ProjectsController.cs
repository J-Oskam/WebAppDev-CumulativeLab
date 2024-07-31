using COMP2139_CumulativeLabs.Areas.ProjectManagement.Models;
using COMP2139_CumulativeLabs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_CumulativeLabs.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]
    public class ProjectsController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        public ProjectsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Projects.ToList());
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var project = _dbContext.Projects
                .FirstOrDefault(p => p.ProjectId == id);
            return View(project);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(project);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var project = _dbContext.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProjectId, Name, Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(project);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(project);
        }

        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var project = _dbContext.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        //[HttpPost("DeleteConfirmed/{projectId:int}")]
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int projectId)
        {
            var project = _dbContext.Projects.Find(projectId);
            if (project != null)
            {
                _dbContext.Remove(project);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        private bool ProjectExists(int id)
        {
            return _dbContext.Projects.Any(p => p.ProjectId == id);
        }

        [HttpGet("Search/{searchString?}")]
        public async Task<IActionResult> Search(string searchString)
        {

            var projectsQuery = from p in _dbContext.Projects
                                select p;

            bool searchPerformed = !string.IsNullOrEmpty(searchString);

            if (searchPerformed)
            {
                projectsQuery = projectsQuery.Where(p => p.Name.Contains(searchString)
                                                || p.Description.Contains(searchString));
            }

            var projects = await projectsQuery.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;
            return View("Index", projects);
        }
    }
}
