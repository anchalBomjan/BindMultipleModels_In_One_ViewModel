using BindMultipleModels_In_One_ViewModel.Web.Data;
using BindMultipleModels_In_One_ViewModel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BindMultipleModels_In_One_ViewModel.Web.Controllers
{

    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Enrollment/Create
        public IActionResult Create()
        {
            ViewData["Students"] = new SelectList(_context.Students, "StudentID", "Name");
            ViewData["Courses"] = new SelectList(_context.Courses, "CourseID", "CourseName");
            return View();
        }

        // POST: /Enrollment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enrollment viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Students"] = new SelectList(_context.Students, "StudentID", "Name", viewModel.StudentID);
                ViewData["Courses"] = new SelectList(_context.Courses, "CourseID", "CourseName", viewModel.CourseID);
                return View(viewModel);
            }

            var enrollment = new Enrollment
            {
                StudentID = viewModel.StudentID,
                CourseID = viewModel.CourseID,
                EnrollmentDate = viewModel.EnrollmentDate
            };
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Enrollment/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var enrollments = await _context.Enrollments.Include(e => e.Student).Include(e => e.Course).ToListAsync();
            return View(enrollments);
        }

        // GET: /Enrollment/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return View("Error", new ErrorViewModel { RequestId = "Invalid Enrollment ID" });
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return View("Error", new ErrorViewModel { RequestId = $"No enrollment found with ID {id}" });
            }

            ViewData["Students"] = new SelectList(_context.Students, "StudentID", "Name", enrollment.StudentID);
            ViewData["Courses"] = new SelectList(_context.Courses, "CourseID", "CourseName", enrollment.CourseID);

            return View(enrollment);
        }

        // POST: /Enrollment/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enrollment viewModel)
        {
            if (id <= 0 || !ModelState.IsValid)
            {
                ViewData["Students"] = new SelectList(_context.Students, "StudentID", "Name", viewModel.StudentID);
                ViewData["Courses"] = new SelectList(_context.Courses, "CourseID", "CourseName", viewModel.CourseID);
                return View(viewModel);
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return View("Error", new ErrorViewModel { RequestId = $"No enrollment found with ID {id}" });
            }

            enrollment.StudentID = viewModel.StudentID;
            enrollment.CourseID = viewModel.CourseID;
            enrollment.EnrollmentDate = viewModel.EnrollmentDate;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Enrollment/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return View("Error", new ErrorViewModel { RequestId = $"No enrollment found with ID {id}" });
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }


}
