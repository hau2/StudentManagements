using ServiceContracts;
using Entities;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
  public class StudentsService : IStudentService
  {
    private readonly List<Student> _students;
    private readonly IClassroomService _classroomService;
    public StudentsService()
    {
      _students = new List<Student>();
      _classroomService = new ClassroomsService();
    }
    private StudentResponse ConvertStudentToStudentResponse(Student student)
    {
      StudentResponse studentResponse = student.ToStudentResponse();
      studentResponse.Classroom = _classroomService.GetClassroomByClassID(student.ClassID)?.ClassName;
      return studentResponse;
    }
    public StudentResponse AddStudent(StudentAddRequest? studentAddRequest)
    {
      if(studentAddRequest == null)
      {
        throw new ArgumentNullException(nameof(studentAddRequest));
      }
      // Model validation
      ValidationHelper.ModelValidation(studentAddRequest);
      // Validation StudentName
      if(string.IsNullOrEmpty(studentAddRequest.StudentName))
      {
        throw new ArgumentException("StudentName can't be blank");
      }
      // Convert studentAddRequest to Student type
      Student student = studentAddRequest.ToStudent();
      // Generate StudentID
      student.StudentID = Guid.NewGuid();
      // Add Student to the list
      _students.Add(student);
      // Convert the Student object into StudentResponse type
      return ConvertStudentToStudentResponse(student);
    }

    public List<StudentResponse> GetAllStudents()
    {
      throw new NotImplementedException();
    }

    public StudentResponse? GetStudentByStudentID(Guid? studentID)
    {
      throw new NotImplementedException();
    }
  }
}
