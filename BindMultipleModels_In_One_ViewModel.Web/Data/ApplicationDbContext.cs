using BindMultipleModels_In_One_ViewModel.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BindMultipleModels_In_One_ViewModel.Web.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option)
		{

		}
		public DbSet<Student> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }



		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
			}
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Student>()
				.HasKey(s => s.StudentID);

			modelBuilder.Entity<Course>()
				.HasKey(c => c.CourseID);

			modelBuilder.Entity<Enrollment>()
				.HasKey(e => e.EnrollmentID);

			modelBuilder.Entity<Enrollment>()
				.HasOne(e => e.Student)
				.WithMany(s => s.Enrollments)
				.HasForeignKey(e => e.StudentID);

			modelBuilder.Entity<Enrollment>()
				.HasOne(e => e.Course)
				.WithMany(c => c.Enrollments)
				.HasForeignKey(e => e.CourseID);

			base.OnModelCreating(modelBuilder);
		}

	}
}
