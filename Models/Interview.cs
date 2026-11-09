
using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalRecruitmentSystem.Models
{
    public class Interview
    {
        public int Id { get; set; }

        [Required]
        public int CandidateId { get; set; }

        public Candidate? Candidate { get; set; }

        [Required]
        public DateTime InterviewDate { get; set; }

        [Required]
        public string InterviewMode { get; set; } = "Offline";

        [Required]
        public string InterviewRound { get; set; } = "HR";

        public string? InterviewPanel { get; set; }

        public string? Evaluation { get; set; }

        public string? Remarks { get; set; }

        public string InterviewResult { get; set; } = "Pending";
        public int? TechnicalKnowledgeScore { get; set; }

        public int? CommunicationSkillsScore { get; set; }

        public int? OverallPerformanceScore { get; set; }

        public string? InterviewerFeedback { get; set; }
    }
}
