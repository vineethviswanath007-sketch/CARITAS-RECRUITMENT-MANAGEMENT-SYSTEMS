using HospitalRecruitmentSystem.Data;
using HospitalRecruitmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalRecruitmentSystem.Controllers
{
    public class EvaluationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // EDIT - GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _context.Interviews.FindAsync(id);

            if (interview == null)
            {
                return NotFound();
            }

            await LoadCandidates();

            return View(interview);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Interview interview)
        {
            if (id != interview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(interview);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadCandidates();

            return View(interview);
        }

        public async Task<IActionResult> Index()
        {
            var evaluations = await _context.Interviews
                .Include(i => i.Candidate)
                .ToListAsync();

            return View(evaluations);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCandidates();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Interview interview)
        {
            if (ModelState.IsValid)
            {
                _context.Interviews.Add(interview);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadCandidates();
            return View(interview);
        }

        private async Task LoadCandidates()
        {
            ViewBag.CandidateId = new SelectList(
                await _context.Candidates.ToListAsync(),
                "Id",
                "FullName"
            );
        }
    }

// DELETE - GET
public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _context.Interviews
                .Include(i => i.Candidate)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);

            if (interview != null)
            {
                _context.Interviews.Remove(interview);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }