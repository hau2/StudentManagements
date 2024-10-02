using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace StudentManagements.Controllers
{
	public class StudentsController : Controller
	{
		private readonly IStudentService _studentService;
		private readonly IClassroomService _classroomService;
		public StudentsController(IStudentService studentService, IClassroomService classroomService)
		{
			_studentService = studentService;
			_classroomService = classroomService;
		}
		[Route("/")]
		[Route("students/index")]
		public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(StudentResponse), SortOrderOptions sortOrder = SortOrderOptions.ASC)
		{
			// Search
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

			// Sort
			students = _studentService.GetSortedStudents(students, sortBy, sortOrder);
			ViewBag.CurrentSortBy = sortBy;
			ViewBag.CurrentSortOrder = sortOrder.ToString();

			return View(students);
		}
		[HttpGet]
		[Route("students/create")]
		public IActionResult Create()
		{
			List<ClassroomResponse> classrooms = _classroomService.GetAllClassrooms();
			ViewBag.Classrooms = classrooms;
			return View();
		}
		[HttpPost]
		[Route("students/create")]
		public IActionResult Create(StudentAddRequest studentAddRequest)
		{
			if(!ModelState.IsValid)
			{
				List<ClassroomResponse> classrooms = _classroomService.GetAllClassrooms();
				ViewBag.Classrooms = classrooms;
				ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).ToList().Select(e => e.ErrorMessage).ToList();
				return View();
			}
			StudentResponse studentResponse = _studentService.AddStudent(studentAddRequest);
			// Navigate to Index() action method (it makes another get request to "students/index"
			return RedirectToAction("Index", "Students");
		}
	}
}
