using BindMultipleModels_In_One_ViewModel.Web.Data;
using BindMultipleModels_In_One_ViewModel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BindMultipleModels_In_One_ViewModel.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var student = new Student
            {
                StudentID = viewModel.StudentID,
                Name = viewModel.Name,
                Email = viewModel.Email
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Student/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        // GET: /Student/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error", new ErrorViewModel { RequestId = "Invalid Student ID" });
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return View("Error", new ErrorViewModel { RequestId = $"No student found with ID {id}" });
            }

            return View(student);
        }

        // POST: /Student/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, StudentViewModel viewModel)
        {
            if (string.IsNullOrEmpty(id) || !ModelState.IsValid)
            {
                return View(viewModel);
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return View("Error", new ErrorViewModel { RequestId = $"No student found with ID {id}" });
            }
            student.StudentID = viewModel.StudentID;
            student.Name = viewModel.Name;
            student.Email = viewModel.Email;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Student/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return View("Error", new ErrorViewModel { RequestId = $"No student found with ID {id}" });
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
