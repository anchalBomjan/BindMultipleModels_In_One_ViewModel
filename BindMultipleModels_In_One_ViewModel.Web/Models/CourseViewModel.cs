using System.ComponentModel.DataAnnotations;

namespace BindMultipleModels_In_One_ViewModel.Web.Models
{
    public class CourseViewModel
    {
        

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        [Required]
        public int Credits { get; set; }


    }
}
