using System.ComponentModel.DataAnnotations;

namespace COMP2139_CumulativeLabs.Models {
    public class Project : IValidatableObject { //To do custom validation you must implement the IValidatableObject interface
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string? Status { get; set; }

        //Navigation property allowing one to many relationship with ProjectTask. Many tasks can belong to one project
        public List<ProjectTask>? ProjectTasks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) { //custom validation to check of EndDate is before StartDate
            if (EndDate < StartDate) {
                //the yield keyword iterates the method to produce a sequence of values
                yield return new ValidationResult("Start date cannot be before end date", new[] { nameof(EndDate), nameof(StartDate) });
            }
        }
    }
}
