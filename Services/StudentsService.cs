using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services
{
  public class StudentsService : IStudentService
  {
    private readonly List<Student> _students;
    private readonly IClassroomService _classroomService;
    public StudentsService(bool initialize = true)
    {
      _students = new List<Student>();
      _classroomService = new ClassroomsService();

      if(initialize)
      {
        _students.AddRange(new List<Student>()
        {
          new Student()
          {
            StudentID = Guid.Parse("DD156302-871C-4B2B-84D4-6A4C4D56EEF8"),
            ClassID = Guid.Parse("8E7E3442-6B8D-4C25-9B2E-054AFF4BCBBE"),
						StudentName = "Student 1",
            Address = "Address 1",
            DateOfBirth = DateTime.Parse("2000-01-01"),
            Email = "1@gmail.com",
            Gender = "Male",
            IsNewCommer = true,
          },
					new Student()
					{
						StudentID = Guid.Parse("D0FEBB69-0B32-45CE-8708-7475FBD92F3C"),
						ClassID = Guid.Parse("8E7E3442-6B8D-4C25-9B2E-054AFF4BCBBE"),
						StudentName = "Student 2",
						Address = "Address 2",
						DateOfBirth = DateTime.Parse("2002-01-01"),
						Email = "2@gmail.com",
						Gender = "Female",
						IsNewCommer = true,
					},
					 new Student()
					{
						StudentID = Guid.Parse("6485FA88-7181-47D9-8594-763DF1222A1C"),
						ClassID = Guid.Parse("ED59FCD0-8DA4-4AD1-952E-86B40DF4E2ED"),
						StudentName = "Student 3",
						Address = "Address 3",
						DateOfBirth = DateTime.Parse("2005-01-01"),
						Email = "3@gmail.com",
						Gender = "Male",
						IsNewCommer = false,
					},
				});
      }
    }
    private StudentResponse ConvertStudentToStudentResponse(Student student)
    {
      StudentResponse studentResponse = student.ToStudentResponse();
      studentResponse.Classroom = _classroomService.GetClassroomByClassID(student.ClassID)?.ClassName;
      return studentResponse;
    }
    public StudentResponse AddStudent(StudentAddRequest? studentAddRequest)
    {
      if (studentAddRequest == null)
      {
        throw new ArgumentNullException(nameof(studentAddRequest));
      }
      // Model validation
      ValidationHelper.ModelValidation(studentAddRequest);
      // Validation StudentName
      if (string.IsNullOrEmpty(studentAddRequest.StudentName))
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
      if (studentID == null) return null;
      Student? student = _students.FirstOrDefault(s => s.StudentID == studentID);
      if (student == null) return null;
      return ConvertStudentToStudentResponse(student);
    }

    public List<StudentResponse> GetFilteredStudents(string searchBy, string? searchString)
    {
      List<StudentResponse> allStudents = GetAllStudents();
      List<StudentResponse> matchingStudents = allStudents;

      if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
      {
        return matchingStudents;
      }

      StringComparison comparison = StringComparison.OrdinalIgnoreCase;
      switch (searchBy)
      {
        case nameof(StudentResponse.StudentName):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.StudentName) ? s.StudentName.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(StudentResponse.Email):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Email) ? s.Email.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(StudentResponse.DateOfBirth):
          matchingStudents = allStudents.Where(s => (s.DateOfBirth != null) ? s.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, comparison) : true).ToList();
          break;
        case nameof(StudentResponse.Gender):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Gender) ? s.Gender.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(StudentResponse.ClassID):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Classroom) ? s.Classroom.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(StudentResponse.Address):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Address) ? s.Address.Contains(searchString, comparison) : true)).ToList();
          break;
        default: matchingStudents = allStudents; break;
      }
      return matchingStudents;
    }

    public List<StudentResponse> GetSortedStudents(List<StudentResponse> allStudents, string sortBy, SortOrderOptions sortOrder)
    {
      if (string.IsNullOrEmpty(sortBy)) return allStudents;
      List<StudentResponse> sortedStudents = (sortBy, sortOrder)
      switch
      {
        (nameof(StudentResponse.StudentName), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.StudentName, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.StudentName), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.StudentName, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Email), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.Email, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Email), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.Email, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.DateOfBirth), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.DateOfBirth).ToList(),
        (nameof(StudentResponse.DateOfBirth), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.DateOfBirth).ToList(),
        (nameof(StudentResponse.Age), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.Age).ToList(),
        (nameof(StudentResponse.Age), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.Age).ToList(),
        (nameof(StudentResponse.Gender), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Gender), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Classroom), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.Classroom, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Classroom), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.Classroom, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Address), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.Address, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Address), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.Address, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.IsNewCommer), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.IsNewCommer).ToList(),
        (nameof(StudentResponse.IsNewCommer), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.IsNewCommer).ToList(),
        _ => allStudents
      };
      return sortedStudents;
    }

    public StudentResponse UpdateStudent(StudentUpdateRequest studentUpdateRequest)
    {
      if(studentUpdateRequest == null) throw new ArgumentNullException(nameof(studentUpdateRequest));
      // validation
      ValidationHelper.ModelValidation(studentUpdateRequest);
      // get matching student object to update
      Student? matchingStudent = _students.FirstOrDefault(s => s.StudentID == studentUpdateRequest.StudentID);
      if (matchingStudent == null) throw new ArgumentException("Given student id doesn't exist");
      // update all details
      matchingStudent.StudentName = studentUpdateRequest.StudentName;
      matchingStudent.Email = studentUpdateRequest.Email;
      matchingStudent.Address = studentUpdateRequest.Address;
      matchingStudent.Gender = studentUpdateRequest.Gender.ToString();
      matchingStudent.DateOfBirth = studentUpdateRequest.DateOfBirth;
      matchingStudent.ClassID = studentUpdateRequest.ClassID;
      matchingStudent.IsNewCommer = studentUpdateRequest.IsNewCommer;

      return matchingStudent.ToStudentResponse();
    }

    public bool DeleteStudent(Guid? studentID)
    {
      if(studentID == null) throw new ArgumentException(nameof(studentID));
      Student? student = _students.FirstOrDefault(s => s.StudentID == studentID);
      if (student == null) return false;
      _students.RemoveAll(s => s.StudentID == studentID);
      return true;
    }
  }
}
