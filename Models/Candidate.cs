
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalRecruitmentSystem.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string CandidateCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Gender { get; set; }

        [Required]
        [StringLength(150)]
        public string Qualification { get; set; } = string.Empty;

        [Range(0, 60)]
        public decimal ExperienceYears { get; set; }

        [Required]
        [StringLength(100)]
        public string AppliedPosition { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [StringLength(30)]
        public string ApplicationStatus { get; set; } = "Applied";

        public string? ResumePath { get; set; }

        public DateTime AppliedOn { get; set; } = DateTime.UtcNow;
        [StringLength(50)]
        public string? Religion { get; set; }

        [StringLength(50)]
        public string? Caste { get; set; }

        [StringLength(150)]
        public string? Place { get; set; }

        [Range(18, 100)]
        public int? Age { get; set; }

        [StringLength(150)]
        public string? LastWorkedOrganization { get; set; }
    }
}
