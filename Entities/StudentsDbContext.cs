using Microsoft.EntityFrameworkCore;

namespace Entities
{
	public class StudentsDbContext : DbContext
	{
		public DbSet<Classroom> Classroom { get; set; }
		public DbSet<Student> Students { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Classroom>().ToTable("Classrooms");
			modelBuilder.Entity<Student>().ToTable("Students");
		}
	}
}
