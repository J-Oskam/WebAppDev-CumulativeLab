using System.ComponentModel.DataAnnotations;

namespace COMP2139_CumulativeLabs.Models {
    public class ProjectTask {
        [Key]
        public int ProjectTaskId { get; set; }

        [Required]
        public string? Title { get; set; } //required with optional ? means it will either have a value or be null

        [Required]
        public string? Description { get; set; }

        //foreign key for Project
        public int ProjectId { get; set; }

        //Navigation property for project. One to many relationship. Has only one project 
        public Project? Project { get; set; }
    }
}
