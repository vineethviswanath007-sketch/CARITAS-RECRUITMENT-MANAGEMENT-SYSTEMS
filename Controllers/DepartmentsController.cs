
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalRecruitmentSystem.Data;
using HospitalRecruitmentSystem.Models;

namespace HospitalRecruitmentSystem.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display all departments
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                .AsNoTracking()
                .OrderBy(d => d.DepartmentName)
                .ToListAsync();

            return View(departments);
        }

        // Open Add Department page
        public IActionResult Create()
        {
            return View();
        }

        // Save new department
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            bool codeExists = await _context.Departments
                .AnyAsync(d => d.DepartmentCode == department.DepartmentCode);

            if (codeExists)
            {
                ModelState.AddModelError(
                    nameof(department.DepartmentCode),
                    "This department code already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(department);
            }

            department.CreatedOn = DateTime.UtcNow;

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // View department details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                return NotFound();

            return View(department);
        }

        // Open Edit Department page
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments.FindAsync(id);

            if (department == null)
                return NotFound();

            return View(department);
        }

        // Save edited department
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, Department department)
        {
            if (id != department.Id)
                return NotFound();

            var existingDepartment =
                await _context.Departments.FindAsync(id);

            if (existingDepartment == null)
                return NotFound();

            bool codeExists = await _context.Departments
                .AnyAsync(d => d.DepartmentCode == department.DepartmentCode
                            && d.Id != id);

            if (codeExists)
            {
                ModelState.AddModelError(
                    nameof(department.DepartmentCode),
                    "This department code already exists.");
            }

            if (!ModelState.IsValid)
                return View(department);

            existingDepartment.DepartmentCode =
                department.DepartmentCode;

            existingDepartment.DepartmentName =
                department.DepartmentName;

            existingDepartment.Description =
                department.Description;

            existingDepartment.IsActive =
                department.IsActive;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Open Delete confirmation page

        // Open Delete confirmation page
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                return NotFound();

            return View(department);
        }

        // Process Delete request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid department ID.");

            var department = await _context.Departments
                .FindAsync(id);

            if (department == null)
                return NotFound();

            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}




