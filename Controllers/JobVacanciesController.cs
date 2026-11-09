
using HospitalRecruitmentSystem.Data;
using HospitalRecruitmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalRecruitmentSystem.Controllers
{
    public class JobVacanciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobVacanciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobVacancies
        public async Task<IActionResult> Index()
        {
            var vacancies = await _context.JobVacancies
                .Include(j => j.Department)
                .ToListAsync();

            return View(vacancies);
        }

        // GET: JobVacancies/Create
        public async Task<IActionResult> Create()
        {
            await LoadDepartments();
            return View();
        }

        // POST: JobVacancies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobVacancy jobVacancy)
        {
            ModelState.Remove(nameof(jobVacancy.Department));

            if (ModelState.IsValid)
            {
                _context.JobVacancies.Add(jobVacancy);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadDepartments(jobVacancy.DepartmentId);
            return View(jobVacancy);
        }

        // GET: JobVacancies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var jobVacancy = await _context.JobVacancies
                .Include(j => j.Department)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jobVacancy == null)
                return NotFound();

            return View(jobVacancy);
        }

        // GET: JobVacancies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var jobVacancy = await _context.JobVacancies
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jobVacancy == null)
                return NotFound();

            await LoadDepartments(jobVacancy.DepartmentId);

            return View(jobVacancy);
        }

        // POST: JobVacancies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobVacancy updatedVacancy)
        {
            if (id != updatedVacancy.Id)
                return NotFound();

            ModelState.Remove(nameof(updatedVacancy.Department));

            if (ModelState.IsValid)
            {
                var jobVacancy = await _context.JobVacancies.FindAsync(id);

                if (jobVacancy == null)
                    return NotFound();

                jobVacancy.JobCode = updatedVacancy.JobCode;
                jobVacancy.JobTitle = updatedVacancy.JobTitle;
                jobVacancy.DepartmentId = updatedVacancy.DepartmentId;
                jobVacancy.JobDescription = updatedVacancy.JobDescription;
                jobVacancy.Qualification = updatedVacancy.Qualification;
                jobVacancy.ExperienceRequired = updatedVacancy.ExperienceRequired;
                jobVacancy.NumberOfVacancies = updatedVacancy.NumberOfVacancies;
                jobVacancy.EmploymentType = updatedVacancy.EmploymentType;
                jobVacancy.SalaryRange = updatedVacancy.SalaryRange;
                jobVacancy.PostedDate = updatedVacancy.PostedDate;
                jobVacancy.ClosingDate = updatedVacancy.ClosingDate;
                jobVacancy.IsActive = updatedVacancy.IsActive;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadDepartments(updatedVacancy.DepartmentId);
            return View(updatedVacancy);
        }

        // GET: JobVacancies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var jobVacancy = await _context.JobVacancies
                .Include(j => j.Department)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jobVacancy == null)
                return NotFound();

            return View(jobVacancy);
        }

        // POST: JobVacancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobVacancy = await _context.JobVacancies.FindAsync(id);

            if (jobVacancy != null)
            {
                _context.JobVacancies.Remove(jobVacancy);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Active Department Dropdown
        private async Task LoadDepartments(int? selectedDepartment = null)
        {
            var departments = await _context.Departments
                .Where(d => d.IsActive)
                .OrderBy(d => d.DepartmentName)
                .ToListAsync();

            ViewBag.DepartmentId = new SelectList(
                departments,
                "Id",
                "DepartmentName",
                selectedDepartment
            );
        }
    }
}
