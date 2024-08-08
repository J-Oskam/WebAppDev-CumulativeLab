using System.ComponentModel.DataAnnotations;

namespace COMP2139_CumulativeLabs.Areas.ProjectManagement.Models
{
    public class Project : IValidatableObject
    { //To do custom validation you must implement the IValidatableObject interface
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [Display(Name = "Project Name")] //will change the label name
        [StringLength(40, MinimumLength = 2, ErrorMessage = "The project name must be between 2 and 40 characters long.")]
        public required string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public string? Status { get; set; }

        //Navigation property allowing one to many relationship with ProjectTask. Many tasks can belong to one project
        public List<ProjectTask>? ProjectTasks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        { //custom validation to check of EndDate is before StartDate
            if (EndDate < StartDate)
            {
                //the yield keyword iterates the method to produce a sequence of values
                yield return new ValidationResult("Start date must be before end date", new[] { nameof(EndDate), nameof(StartDate) });
            }
        }
    }
}
