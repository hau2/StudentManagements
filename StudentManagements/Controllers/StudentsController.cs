using Microsoft.AspNetCore.Mvc;

namespace StudentManagements.Controllers
{
	public class StudentsController : Controller
	{
		[Route("/")]
		[Route("students/index")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
