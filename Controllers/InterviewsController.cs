
using HospitalRecruitmentSystem.Data;
using HospitalRecruitmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalRecruitmentSystem.Controllers
{
    public class InterviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Interviews
        public async Task<IActionResult> Index()
        {
            var interviews = await _context.Interviews
                .Include(i => i.Candidate)
                .OrderByDescending(i => i.InterviewDate)
                .ToListAsync();

            return View(interviews);
        }

        // GET: Interviews/Create
        public async Task<IActionResult> Create()
        {
            await LoadCandidates();
            return View();
        }

        // POST: Interviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Interview interview)
        {
            ModelState.Remove(nameof(interview.Candidate));

            if (ModelState.IsValid)
            {
                _context.Interviews.Add(interview);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadCandidates(interview.CandidateId);
            return View(interview);
        }

        // GET: Interviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var interview = await _context.Interviews
                .Include(i => i.Candidate)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (interview == null)
                return NotFound();

            return View(interview);
        }

        // Candidate Dropdown
        // GET: Interviews/Evaluation/5
        public async Task<IActionResult> Evaluation(int? id)
        {
            if (id == null)
                return NotFound();

            var interview = await _context.Interviews
                .FirstOrDefaultAsync(i => i.Id == id);

            if (interview == null)
                return NotFound();

            return View(interview);
        }

        // POST: Interviews/Evaluation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Evaluation(Interview interview)
        {
            var existingInterview = await _context.Interviews
                .FirstOrDefaultAsync(i => i.Id == interview.Id);

            if (existingInterview == null)
                return NotFound();

            existingInterview.TechnicalKnowledgeScore = interview.TechnicalKnowledgeScore;
            existingInterview.CommunicationSkillsScore = interview.CommunicationSkillsScore;
            existingInterview.OverallPerformanceScore = interview.OverallPerformanceScore;
            existingInterview.InterviewerFeedback = interview.InterviewerFeedback;
            existingInterview.InterviewResult = interview.InterviewResult;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private async Task LoadCandidates(int? selectedCandidate = null)
        {
            var candidates = await _context.Candidates
                .OrderBy(c => c.FullName)
                .ToListAsync();

            ViewBag.CandidateId = new SelectList(
                candidates,
                "Id",
                "FullName",
                selectedCandidate
            );
        }
    }
}
