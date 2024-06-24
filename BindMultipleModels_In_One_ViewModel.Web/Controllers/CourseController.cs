using BindMultipleModels_In_One_ViewModel.Web.Data;
using BindMultipleModels_In_One_ViewModel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BindMultipleModels_In_One_ViewModel.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var course = new Course
            {
                CourseName = viewModel.CourseName,
                CourseDescription = viewModel.CourseDescription,
                Credits = viewModel.Credits
            };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Course/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        // GET: /Course/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return View("Error", new ErrorViewModel { RequestId = "Invalid Course ID" });
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return View("Error", new ErrorViewModel { RequestId = $"No course found with ID {id}" });
            }

            var viewModel = new CourseViewModel
            {
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                Credits = course.Credits
            };

            return View(viewModel);
        }

        // POST: /Course/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel viewModel)
        {
            if (id <= 0 || !ModelState.IsValid)
            {
                return View(viewModel);
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return View("Error", new ErrorViewModel { RequestId = $"No course found with ID {id}" });
            }

            course.CourseName = viewModel.CourseName;
            course.CourseDescription = viewModel.CourseDescription;
            course.Credits = viewModel.Credits;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Course/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return View("Error", new ErrorViewModel { RequestId = $"No course found with ID {id}" });
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
