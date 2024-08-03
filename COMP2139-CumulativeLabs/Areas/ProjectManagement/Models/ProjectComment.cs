using System.ComponentModel.DataAnnotations;

namespace COMP2139_CumulativeLabs.Areas.ProjectManagement.Models {
    public class ProjectComment {

        [Key]
        public int CommentId { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Content { get; set; }

        [Display(Name = "Posted date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        //foreign key
        public int ProjectId { get; set; }

        //navigation property
        public Project? Project { get; set; }
    }
}
