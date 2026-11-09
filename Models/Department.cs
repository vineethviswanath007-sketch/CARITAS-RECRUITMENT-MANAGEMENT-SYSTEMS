
using System.ComponentModel.DataAnnotations;

namespace HospitalRecruitmentSystem.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department code is required")]
        [StringLength(20)]
        [Display(Name = "Department Code")]
        public string DepartmentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }

        [Display(Name = "Active Department")]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
