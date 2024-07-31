using Microsoft.AspNetCore.Mvc;
using COMP2139_CumulativeLabs.Areas.ProjectManagement.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using COMP2139_CumulativeLabs.Data;

namespace COMP2139_CumulativeLabs.Areas.ProjectManagement.Components.ProjectSummary {
    public class ProjectSummaryViewComponent : ViewComponent {
        private readonly ApplicationDbContext _dbContext;
        public ProjectSummaryViewComponent(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int projectId) {
            var project = await _dbContext.Projects
                .Include(p => p.ProjectTasks)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if(project == null) {
                return Content("Project not found.");
            }

            return View(project);
        }
    }
}