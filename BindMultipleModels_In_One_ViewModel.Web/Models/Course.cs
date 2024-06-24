using System.ComponentModel.DataAnnotations;

namespace BindMultipleModels_In_One_ViewModel.Web.Models
{
	public class Course
	{
		[Key]
		public int CourseID { get; set; }

		[Required]
		[StringLength(100)]
		public string CourseName { get; set; }

		public string CourseDescription { get; set; }

		[Required]
		public int Credits { get; set; }

		public ICollection<Enrollment> Enrollments { get; set; }
	}

}
