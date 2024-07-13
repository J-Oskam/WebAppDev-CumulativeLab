using System.ComponentModel.DataAnnotations;

namespace COMP2139_CumulativeLabs.Models {
    public class Project {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string? Status { get; set; }
    }
}
