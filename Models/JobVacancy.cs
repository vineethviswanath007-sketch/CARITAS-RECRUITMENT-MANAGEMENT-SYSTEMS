using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalRecruitmentSystem.Models
{
    public class JobVacancy
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string JobCode { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [Required]
        public string JobDescription { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Qualification { get; set; } = string.Empty;

        [Range(0, 60)]
        public decimal ExperienceRequired { get; set; }

        [Range(1, 1000)]
        public int NumberOfVacancies { get; set; }

        [Required]
        [StringLength(30)]
        public string EmploymentType { get; set; } = "Full-Time";

        [StringLength(100)]
        public string? SalaryRange { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        public DateTime ClosingDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}