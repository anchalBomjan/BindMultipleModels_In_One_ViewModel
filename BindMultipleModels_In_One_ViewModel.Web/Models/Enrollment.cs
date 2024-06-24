using System.ComponentModel.DataAnnotations;

namespace BindMultipleModels_In_One_ViewModel.Web.Models
{
	public class Enrollment
	{
		[Key]
		public int EnrollmentID { get; set; }

		[Required]
		[StringLength(20)]
		public string StudentID { get; set; }

		[Required]
		public int CourseID { get; set; }

		[Required]
		public DateTime EnrollmentDate { get; set; }

		public Student Student { get; set; }
		public Course Course { get; set; }
	}

}
