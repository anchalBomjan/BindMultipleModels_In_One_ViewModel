using BindMultipleModels_In_One_ViewModel.Web.Data;
using BindMultipleModels_In_One_ViewModel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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
		public async Task< IActionResult> Create(CourseViewModel viewModel)
		{
            var course = new Course
            {
                CourseName = viewModel.CourseName,
                CourseDescription = viewModel.CourseDescription,
                Credits = viewModel.Credits
                
            };
			await _context.Courses.AddAsync(course);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index","Course");
        }


        [HttpGet]
        

        public IActionResult Index()
		{
			var courses = _context.Courses.ToList();
			return View(courses);
		}



        [HttpGet]
        public async Task<IActionResult> Edit(int CourseID)
        {
            var course = await _context.Courses.FindAsync( CourseID);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course viewModel)
        {
            var course = await _context.Courses.FindAsync(viewModel.CourseID);

            if ( course is not null)
            {
                course.CourseName = viewModel.CourseName;
                course.CourseDescription = viewModel.CourseDescription;
                course.Credits = viewModel.Credits;


                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index ", "Course");


        }


        [HttpPost]
        public async Task<IActionResult> Delete(Course viewModel)
        {
            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CourseID == viewModel.CourseID);
            if (course is not null)
            {
                _context.Courses.Remove(viewModel);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("Index", "Course");
        }

    }
}
