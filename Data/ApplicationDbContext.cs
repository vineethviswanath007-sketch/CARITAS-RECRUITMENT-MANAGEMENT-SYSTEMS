using HospitalRecruitmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalRecruitmentSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Candidate> Candidates { get; set; } = null!;
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<JobVacancy> JobVacancies { get; set; } = null!;
        public DbSet<JobApplication> JobApplications { get; set; } = null!;
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.DepartmentCode)
                .IsUnique();

            modelBuilder.Entity<JobApplication>()
                .HasOne(j => j.Candidate)
                .WithMany()
                .HasForeignKey(j => j.CandidateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JobApplication>()
                .HasOne(j => j.JobVacancy)
                .WithMany()
                .HasForeignKey(j => j.JobVacancyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}