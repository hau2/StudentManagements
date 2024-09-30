using ServiceContracts;
using Entities;
using ServiceContracts.DTO;
using Services.Helpers;
using ServiceContracts.Enums;

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
      return _students.Select(s => s.ToStudentResponse()).ToList();
    }

    public StudentResponse? GetStudentByStudentID(Guid? studentID)
    {
      if(studentID == null) return null;
      Student? student = _students.FirstOrDefault(s => s.StudentID == studentID);
      if(student == null) return null;
      return ConvertStudentToStudentResponse(student);
    }

    public List<StudentResponse> GetFilteredStudents(string searchBy, string? searchString)
    {
      List<StudentResponse> allStudents = GetAllStudents();
      List<StudentResponse> matchingStudents = allStudents;

      if(string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
      {
        return matchingStudents;
      }

      StringComparison comparison = StringComparison.OrdinalIgnoreCase;
      switch (searchBy)
      {
        case nameof(Student.StudentName):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.StudentName) ? s.StudentName.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(Student.Email):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Email) ? s.Email.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(Student.DateOfBirth):
          matchingStudents = allStudents.Where(s => (s.DateOfBirth != null) ? s.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, comparison) : true).ToList();
          break;
        case nameof(Student.Gender):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Gender) ? s.Gender.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(Student.ClassID):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Classroom) ? s.Classroom.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(Student.Address):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Address) ? s.Address.Contains(searchString, comparison) : true)).ToList();
          break;
        default: matchingStudents = allStudents; break;
      }
      return matchingStudents;
    }

    public List<StudentResponse> GetSortedStudents(List<StudentResponse> allStudents, string sortBy, SortOrderOptions sortOrder)
    {
      throw new NotImplementedException();
    }
  }
}
