using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace StudentManagements.Controllers
{
	public class StudentsController : Controller
	{
		private readonly IStudentService _studentService;
		public StudentsController(IStudentService studentService)
		{
			_studentService = studentService;
		}
		[Route("/")]
		[Route("students/index")]
		public IActionResult Index(string searchBy, string? searchString)
		{
			ViewBag.SearchFields = new Dictionary<string, string>()
			{
				{ nameof(StudentResponse.StudentName), "Student Name" },
				{ nameof(StudentResponse.Email), "Email" },
				{ nameof(StudentResponse.DateOfBirth), "Date of Birth" },
				{ nameof(StudentResponse.Gender), "Gender" },
				{ nameof(StudentResponse.ClassID), "Classroom" },
				{ nameof(StudentResponse.Address), "Address" }
			};
			List<StudentResponse> students = _studentService.GetFilteredStudents(searchBy, searchString);
			ViewBag.CurrentSearchBy = searchBy;
			ViewBag.CurrentSearchString = searchString;
			return View(students);
		}
	}
}
