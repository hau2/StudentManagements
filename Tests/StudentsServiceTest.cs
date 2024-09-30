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
    #region GetStudentByStudentID
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
    #region GetAllStudent
    [Fact]
    public void GetAllStudents_EmptyList()
    {
      // Act
      List<StudentResponse> studentResponses = _studentService.GetAllStudents();
      // Assert
      Assert.Empty(studentResponses);
    }
    [Fact]
    public void GetAllStudents_AddFewStudents()
    {
      // Arrange
      ClassroomAddRequest classroomAddRequest1 = new ClassroomAddRequest() { ClassName = "DSA" };
      ClassroomAddRequest classroomAddRequest2 = new ClassroomAddRequest() { ClassName = "Database" };

      ClassroomResponse classroomResponse1 = _classroomService.AddClassroom(classroomAddRequest1);
      ClassroomResponse classroomResponse2 = _classroomService.AddClassroom(classroomAddRequest2);

      StudentAddRequest studentAddRequest1 = new StudentAddRequest()
      {
        StudentName = "Example name",
        Address = "Example address",
        Email = "person@example.com",
        ClassID = classroomResponse2.ClassID,
        Gender = GenderOptions.Male,
        DateOfBirth = DateTime.Parse("2000-01-01"),
        IsNewCommer = true,
      };
      StudentAddRequest studentAddRequest2 = new StudentAddRequest()
      {
        StudentName = "Example name 2",
        Address = "Example address 2",
        Email = "person2@example.com",
        ClassID = classroomResponse2.ClassID,
        Gender = GenderOptions.Male,
        DateOfBirth = DateTime.Parse("2000-01-01"),
        IsNewCommer = true,
      };
      StudentAddRequest studentAddRequest3 = new StudentAddRequest()
      {
        StudentName = "Example name 2",
        Address = "Example address 2",
        Email = "person2@example.com",
        ClassID = classroomResponse1.ClassID,
        Gender = GenderOptions.Male,
        DateOfBirth = DateTime.Parse("2000-01-01"),
        IsNewCommer = true,
      };

      List<StudentAddRequest> studentAddRequests = new List<StudentAddRequest>() { studentAddRequest1, studentAddRequest2, studentAddRequest3 };
      List<StudentResponse> studentAddFromAdd = new List<StudentResponse>();
      foreach (StudentAddRequest studentAddRequest in studentAddRequests)
      {
        StudentResponse studentResponse = _studentService.AddStudent(studentAddRequest);
        studentAddFromAdd.Add(studentResponse);
      }

      //Act
      List<StudentResponse> studentListFromGet = _studentService.GetAllStudents();
      //Assert
      foreach(StudentResponse studentResponse in studentAddFromAdd)
      {
        Assert.Contains(studentResponse, studentListFromGet);
      }
    }
    #endregion
  }
}
