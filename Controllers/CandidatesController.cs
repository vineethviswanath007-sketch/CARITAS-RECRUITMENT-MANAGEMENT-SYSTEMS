using HospitalRecruitmentSystem.Data;
using HospitalRecruitmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace HospitalRecruitmentSystem.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Candidates

        public async Task<IActionResult> Index(string searchString, int? departmentId, string status)
        {
            var candidates = _context.Candidates
                .Include(c => c.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                candidates = candidates.Where(c =>
                    c.FullName.Contains(searchString) ||
                    c.Email.Contains(searchString) ||
                    c.Phone.Contains(searchString));
            }

            if (departmentId.HasValue)
            {
                candidates = candidates.Where(c => 
                   c.DepartmentId == departmentId.Value);
            }

            if (!string.IsNullOrEmpty(status))
            {
                candidates = candidates.Where(c => c.ApplicationStatus == status);
            }

            ViewBag.Departments = await _context.Departments.ToListAsync();
            ViewBag.SelectedDepartment = departmentId;
            ViewBag.SelectedStatus = status;
            ViewBag.SearchString = searchString;

            return View(await candidates.ToListAsync());
        }


        // GET: Candidates/Create
        public async Task<IActionResult> Create()
        {
            await LoadDepartments();
            return View();
        }

        // POST: Candidates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Candidate candidate)
        {
            ModelState.Remove(nameof(candidate.CandidateCode));
            ModelState.Remove(nameof(candidate.Department));

            if (ModelState.IsValid)
            {
                candidate.CandidateCode =
                    "CAND-" + Guid.NewGuid().ToString("N")[..8].ToUpper();

                candidate.AppliedOn = DateTime.UtcNow;
                candidate.ApplicationStatus = "Applied";

                _context.Candidates.Add(candidate);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadDepartments(candidate.DepartmentId);
            return View(candidate);
        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var candidate = await _context.Candidates
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null)
                return NotFound();

            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null)
                return NotFound();

            await LoadDepartments(candidate.DepartmentId);

            return View(candidate);
        }

        // POST: Candidates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Candidate updatedCandidate)
        {
            if (id != updatedCandidate.Id)
                return NotFound();

            ModelState.Remove(nameof(updatedCandidate.CandidateCode));
            ModelState.Remove(nameof(updatedCandidate.Department));
            ModelState.Remove(nameof(updatedCandidate.AppliedOn));
            ModelState.Remove(nameof(updatedCandidate.ApplicationStatus));

            if (ModelState.IsValid)
            {
                var candidate = await _context.Candidates
                    .FindAsync(id);

                if (candidate == null)
                    return NotFound();

                candidate.FullName = updatedCandidate.FullName;
                candidate.Email = updatedCandidate.Email;
                candidate.Phone = updatedCandidate.Phone;
                candidate.Gender = updatedCandidate.Gender;
                candidate.Qualification = updatedCandidate.Qualification;
                candidate.ExperienceYears = updatedCandidate.ExperienceYears;
                candidate.AppliedPosition = updatedCandidate.AppliedPosition;
                candidate.DepartmentId = updatedCandidate.DepartmentId;
                candidate.Religion = updatedCandidate.Religion;
                candidate.Caste = updatedCandidate.Caste;
                candidate.Place = updatedCandidate.Place;
                candidate.Age = updatedCandidate.Age;
                candidate.LastWorkedOrganization = updatedCandidate.LastWorkedOrganization;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadDepartments(updatedCandidate.DepartmentId);

            return View(updatedCandidate);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var candidate = await _context.Candidates
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null)
                return NotFound();

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);

            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Department Dropdown
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

       
[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(int id, Candidate candidate)
        {
            if (id != candidate.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadDepartments(candidate.DepartmentId);
                return View(candidate);
            }

            try
            {
                _context.Update(candidate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Candidates.Any(c => c.Id == candidate.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }
    }

}