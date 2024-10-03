using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

			// Seed to Classrooms
			string classroomsJson = File.ReadAllText("classrooms.json");
			List<Classroom> classrooms = JsonSerializer.Deserialize<List<Classroom>>(classroomsJson);

			foreach(Classroom classroom in classrooms)
			{
				modelBuilder.Entity<Classroom>().HasData(classroom);
			}

			// Seed to Students
			string studentsJson = File.ReadAllText("students.json");
			List<Student> students = JsonSerializer.Deserialize<List<Student>>(studentsJson);

			foreach (Student student in students)
			{
				modelBuilder.Entity<Student>().HasData(student);
			}
		}
	}
}
