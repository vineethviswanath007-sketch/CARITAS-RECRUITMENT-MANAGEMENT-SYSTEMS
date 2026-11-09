using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalRecruitmentSystem.Models
{
    public class JobOffer
    {
        public int Id { get; set; }

        [Required]
        public int CandidateId { get; set; }

        public Candidate? Candidate { get; set; }

        public decimal OfferedSalary { get; set; }

        public DateTime OfferDate { get; set; }

        public DateTime? JoiningDate { get; set; }

        public string OfferStatus { get; set; } = "Pending";

        public string? Remarks { get; set; }
    }
}