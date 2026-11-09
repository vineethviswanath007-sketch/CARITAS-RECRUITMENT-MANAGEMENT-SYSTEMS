using HospitalRecruitmentSystem.Data;
using HospitalRecruitmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalRecruitmentSystem.Controllers
{
    public class JobOffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobOffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var offers = await _context.JobOffers
                .Include(o => o.Candidate)
                .ToListAsync();

            return View(offers);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCandidates();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobOffer offer)
        {
            ModelState.Remove(nameof(offer.Candidate));

            if (ModelState.IsValid)
            {
                _context.JobOffers.Add(offer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadCandidates(offer.CandidateId);
            return View(offer);
        }
        // GET: JobOffers/OfferLetter/5
        public async Task<IActionResult> OfferLetter(int? id)
        {
            if (id == null)
                return NotFound();

            var offer = await _context.JobOffers
                .Include(o => o.Candidate)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null)
                return NotFound();

            return View(offer);
        }
        // GET: JobOffers/UpdateStatus/5
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            if (id == null)
                return NotFound();

            var offer = await _context.JobOffers
                .Include(o => o.Candidate)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null)
                return NotFound();

            return View(offer);
        }

        // POST: JobOffers/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string offerStatus)
        {
            var offer = await _context.JobOffers.FindAsync(id);

            if (offer == null)
                return NotFound();

            offer.OfferStatus = offerStatus;

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