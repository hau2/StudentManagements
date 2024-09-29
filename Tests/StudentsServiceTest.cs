using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;

namespace Tests
{
  public class StudentsServiceTest
  {
    private readonly IStudentService _studentService;
    public StudentsServiceTest()
    {
      _studentService = new StudentsService();
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
  }
}
