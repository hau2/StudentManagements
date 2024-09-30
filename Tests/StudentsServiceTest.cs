using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace Tests
{
  public class StudentsServiceTest
  {
    private readonly IStudentService _studentService;
    private readonly IClassroomService _classroomService;
    public StudentsServiceTest()
    {
      _studentService = new StudentsService();
      _classroomService = new ClassroomsService();
    }
    #region AddStudent
    [Fact]
    public void AddStudent_NullStudent()
    {
      // Arrange
      StudentAddRequest? request = null;
      // Act
      Assert.Throws<ArgumentNullException>(() =>
      {
        _studentService.AddStudent(request);
      });
    }
    [Fact]
    public void AddStudent_StudentNameIsNull()
    {
      // Arrange
      StudentAddRequest? studentAddRequest = new StudentAddRequest()
      {
        StudentName = null
      };
      // Act
      Assert.Throws<ArgumentException>(() =>
      {
        _studentService.AddStudent(studentAddRequest);
      });
    }
    [Fact]
    public void AddStudent_ProperStudentDetails()
    {
      // Arrange
      StudentAddRequest? studentAddRequest = new StudentAddRequest()
      {
        StudentName = "Example name",
        Address = "Example address",
        Email = "person@example.com",
        ClassID = new Guid(),
        Gender = GenderOptions.Male,
        DateOfBirth = DateTime.Parse("2000-01-01"),
        IsNewCommer = true,
      };
      // Act
      StudentResponse student_response_from_add = _studentService.AddStudent(studentAddRequest);
      List<StudentResponse> student_list = _studentService.GetAllStudents();
      // Assert
      Assert.True(student_response_from_add.StudentID != Guid.Empty);
      Assert.Contains(student_response_from_add, student_list);
    }
    #endregion
    #region
    [Fact]
    public void GetStudentByStudentID_NullStudentID()
    {
      // Arrange
      Guid? studentID = null;
      // Atc
      StudentResponse? student_response_from_get = _studentService.GetStudentByStudentID(studentID);
      // Assert
      Assert.Null(student_response_from_get);
    }
    [Fact]
    public void GetStudentByStudentID_WithStudentID()
    {
      // Arrange
      ClassroomAddRequest classroomAddRequest = new ClassroomAddRequest() { ClassName = "DSA"};
      ClassroomResponse classroomResponse = _classroomService.AddClassroom(classroomAddRequest);
      // Act
      StudentAddRequest studentAddRequest = new StudentAddRequest()
      {
        StudentName = "Test",
        Email = "Email@example.com",
        Address = "Address",
        ClassID = classroomResponse.ClassID,
        Gender = GenderOptions.Male,
        DateOfBirth = DateTime.Parse("2000-01-01"),
        IsNewCommer = true,
      };

      StudentResponse studentResponseFromAdd = _studentService.AddStudent(studentAddRequest);
      StudentResponse? studentResponseFromGet = _studentService.GetStudentByStudentID(studentResponseFromAdd.StudentID);
      // Assert
      Assert.Equal(studentResponseFromAdd, studentResponseFromGet);
    }
    #endregion
  }
}
