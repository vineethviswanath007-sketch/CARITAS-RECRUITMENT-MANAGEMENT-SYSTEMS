
using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalRecruitmentSystem.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationNumber { get; set; }

        [Required]
        public int CandidateId { get; set; }

        public Candidate? Candidate { get; set; }

        [Required]
        public int JobVacancyId { get; set; }

        public JobVacancy? JobVacancy { get; set; }

        public DateTime ApplicationDate { get; set; } = DateTime.Today;

        public string? ResumePath { get; set; }

        public string ApplicationStatus { get; set; } = "Submitted";

        public string InterviewStatus { get; set; } = "Pending";

        public string? HRRemarks { get; set; }
    }
}
