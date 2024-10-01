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
		public IActionResult Index()
		{
			List<StudentResponse> students = _studentService.GetAllStudents();
			return View(students);
		}
	}
}
