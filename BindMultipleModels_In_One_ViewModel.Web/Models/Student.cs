﻿using System.ComponentModel.DataAnnotations;

namespace BindMultipleModels_In_One_ViewModel.Web.Models
{

	public class Student
	{
		[Key]
		[StringLength(20)]
		public string StudentID { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(100)]
		public string Email { get; set; }

		public ICollection<Enrollment> Enrollments { get; set; }
	}



}