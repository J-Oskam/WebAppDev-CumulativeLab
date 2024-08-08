using System.ComponentModel.DataAnnotations;

namespace COMP2139_CumulativeLabs.Areas.ProjectManagement.Models
{
    public class ProjectTask
    {
        [Key]
        public int ProjectTaskId { get; set; }

        [Required]
        [Display(Name = "Task Name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "The Task name must be between 2 and 40 characters long.")]
        public string? Title { get; set; } //required with optional ? means it will either have a value or be null

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        //foreign key for Project
        public int ProjectId { get; set; }

        //Navigation property for project. One to many relationship. Has only one project 
        public Project? Project { get; set; }
    }
}
