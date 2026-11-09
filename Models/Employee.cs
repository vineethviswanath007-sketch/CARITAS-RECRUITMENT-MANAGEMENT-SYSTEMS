
using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalRecruitmentSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string Designation { get; set; }

        public string Department { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set; }

        public string Address { get; set; }

        public string Qualification { get; set; }

        public string PreviousOrganization { get; set; }

        public string DocumentVerification { get; set; }

        public string MedicalFitnessStatus { get; set; }

        public string BackgroundVerification { get; set; }

        public string OnboardingStatus { get; set; }
    }
}
