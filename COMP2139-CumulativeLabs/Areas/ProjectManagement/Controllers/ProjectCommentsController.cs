using COMP2139_CumulativeLabs.Areas.ProjectManagement.Models;
using COMP2139_CumulativeLabs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_CumulativeLabs.Areas.ProjectManagement.Controllers {

    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]
    public class ProjectCommentsController : Controller {

        private readonly ApplicationDbContext _dbContext;

        public ProjectCommentsController(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int projectId) {
            var comments = await _dbContext.ProjectComments
                .Where(c => c.ProjectId == projectId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();

            return Json(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] ProjectComment comment) {
            if (ModelState.IsValid) {
                comment.CreatedDate = DateTime.Now; //sets date to whatever time is at time of creation. User doens't set the created time themselves.
                _dbContext.ProjectComments.Add(comment);
                await _dbContext.SaveChangesAsync();

                return Json( new { success = true, message = "Comment added successfully" });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json( new { success = false, message = "Comment could not be added.", error = errors });
        }
    }
}