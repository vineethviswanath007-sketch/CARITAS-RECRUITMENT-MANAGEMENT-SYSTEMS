
using HospitalRecruitmentSystem.Data;
using HospitalRecruitmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalRecruitmentSystem.Controllers
{
    public class JobApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobApplications
        public async Task<IActionResult> Index()
        {
            var applications = await _context.JobApplications
                .Include(a => a.Candidate)
                .Include(a => a.JobVacancy)
                .ToListAsync();

            return View(applications);
        }

        // Load dropdowns
        private async Task LoadDropdowns()
        {
            ViewBag.CandidateId = new SelectList(
                await _context.Candidates.ToListAsync(),
                "Id",
                "FullName");

            ViewBag.JobVacancyId = new SelectList(
                await _context.JobVacancies.ToListAsync(),
                "Id",
                "JobTitle");
        }

        // GET: JobApplications/Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View();
        }



        // POST: JobApplications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobApplication application)
        {
            if (string.IsNullOrWhiteSpace(application.ApplicationNumber))
            {
                application.ApplicationNumber =
                    "APP" + DateTime.Now.ToString("yyyyMMddHHmmss");

                ModelState.Remove(nameof(application.ApplicationNumber));
            }

            application.ApplicationDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .Select(x => x.Key + ": " +
                        string.Join(", ", x.Value!.Errors.Select(e => e.ErrorMessage)));

                await LoadDropdowns();
                ViewBag.ValidationErrors = string.Join("\n", errors);

                return View(application);
            }

            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: JobApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.JobApplications
                .Include(a => a.Candidate)
                .Include(a => a.JobVacancy)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

    }
}