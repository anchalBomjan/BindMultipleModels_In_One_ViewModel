namespace BindMultipleModels_In_One_ViewModel.Web.Models
{
    public class StudentCourseEnrollmentViewModel
    {
        public Student Student { get; set; }
        public List<Course> Courses { get; set; }
        public Enrollment Enrollment { get; set; }

    }
}
