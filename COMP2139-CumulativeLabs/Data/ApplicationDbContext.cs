using COMP2139_CumulativeLabs.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_CumulativeLabs.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { //one to many relationship between project and project task defined here as well in their respective Models
            modelBuilder.Entity<Project>()
                .HasMany(p => p.ProjectTasks)
                .WithOne(pt => pt.Project)
                .HasForeignKey(pt => pt.ProjectId);
        }
    }
}