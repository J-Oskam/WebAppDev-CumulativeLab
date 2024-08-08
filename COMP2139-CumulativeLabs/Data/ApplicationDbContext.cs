using COMP2139_CumulativeLabs.Areas.ProjectManagement.Controllers;
using COMP2139_CumulativeLabs.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_CumulativeLabs.Data
{
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder) { //one to many relationship between project and project task defined here as well in their respective Models
        //    modelBuilder.Entity<Project>()
        //        .HasMany(p => p.ProjectTasks)
        //        .WithOne(pt => pt.Project)
        //        .HasForeignKey(pt => pt.ProjectId);
        //}
    }
}