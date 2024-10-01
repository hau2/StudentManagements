using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace Tests
{
  public class StudentsServiceTest
  {
    private readonly IStudentService _studentService;
    private readonly IClassroomService _classroomService;
    private readonly ITestOutputHelper _testOutputHelper;
    public StudentsServiceTest(ITestOutputHelper testOutputHelper)
    {
      _testOutputHelper = testOutputHelper;
      _studentService = new StudentsService();
      _classroomService = new ClassroomsService(false);
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
      ClassroomAddRequest classroomAddRequest = new ClassroomAddRequest() { ClassName = "DSA" };
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
      #region PrintResult
      //print studentAddFromAdd
      _testOutputHelper.WriteLine("Expected:");
      foreach (StudentResponse s in studentAddFromAdd)
      {
        _testOutputHelper.WriteLine(s.ToString());
      }
      //Act
      List<StudentResponse> studentListFromGet = _studentService.GetAllStudents();
      //print studentListFromSort
      _testOutputHelper.WriteLine("Actual:");
      foreach (StudentResponse s in studentListFromGet)
      {
        _testOutputHelper.WriteLine(s.ToString());
      }
      #endregion
      //Assert
      foreach (StudentResponse studentResponse in studentAddFromAdd)
      {
        Assert.Contains(studentResponse, studentListFromGet);
      }
    }
    #endregion
    #region GetFilteredStudents
    [Fact]
    public void GetFilteredStudents_EmptySearchText()
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
      #region PrintResult
      //print studentAddFromAdd
      _testOutputHelper.WriteLine("Expected:");
      foreach (StudentResponse s in studentAddFromAdd)
      {
        _testOutputHelper.WriteLine(s.ToString());
      }
      #endregion
      //Act
      List<StudentResponse> studentListFromSearch = _studentService.GetFilteredStudents(nameof(Student.StudentName), "");
      #region PrintResult
      _testOutputHelper.WriteLine("Actual:");
      foreach (StudentResponse s in studentListFromSearch)
      {
        _testOutputHelper.WriteLine(s.ToString());
      }
      #endregion
      //Assert
      foreach (StudentResponse studentResponse in studentAddFromAdd)
      {
        Assert.Contains(studentResponse, studentListFromSearch);
      }
    }
    [Fact]
    public void GetFilteredStudents_WithSearchByPersonName()
    {
      // Arrange
      string searchValue = "Example name 2";
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
      #region PrintResult
      //print studentAddFromAdd
      _testOutputHelper.WriteLine("Expected:");
      foreach (StudentResponse s in studentAddFromAdd)
      {
        _testOutputHelper.WriteLine(s.ToString());
      }
      #endregion
      //Act
      List<StudentResponse> studentListFromSearch = _studentService.GetFilteredStudents(nameof(Student.StudentName), searchValue);
      #region PrintResult
      _testOutputHelper.WriteLine("Actual:");
      foreach (StudentResponse s in studentListFromSearch)
      {
        _testOutputHelper.WriteLine(s.ToString());
      }
      #endregion
      //Assert
      foreach (StudentResponse s in studentAddFromAdd)
      {
        if(s.StudentName != null)
        {
          if (s.StudentName.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
          {
            Assert.Contains(s, studentListFromSearch);
          }
        }
      }
    }
    #endregion
    #region GetSortedStudents
    [Fact]
    public void GetSortedStudents()
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
      studentAddFromAdd = studentAddFromAdd.OrderByDescending(x => x.StudentName).ToList();
      #region PrintResult
      //print studentAddFromAdd
      _testOutputHelper.WriteLine("Expected:");
      foreach (StudentResponse s in studentAddFromAdd)
      {
        _testOutputHelper.WriteLine(s.ToString());
      }
      #endregion
      List<StudentResponse> allStudents = _studentService.GetAllStudents();
      //Act
      List<StudentResponse> studentListFromSort = _studentService.GetSortedStudents(allStudents, nameof(Student.StudentName), SortOrderOptions.DESC);
      #region PrintResult
      _testOutputHelper.WriteLine("Actual:");
      foreach (StudentResponse s in studentListFromSort)
      {
        _testOutputHelper.WriteLine(s.ToString());
      }
      #endregion
      //Assert
      for (int i = 0; i < studentListFromSort.Count; i++)
      {
        Assert.Equal(studentAddFromAdd[i], studentListFromSort[i]);
      }
    }
    #endregion
    #region UpdateStudent
    [Fact]
    public void UpdateStudent_NullStudent()
    {
      // Arrange
      StudentUpdateRequest studentUpdateRequest = null;
      // Assert
      Assert.Throws<ArgumentNullException>(() => _studentService.UpdateStudent(studentUpdateRequest));
    }
    [Fact]
    public void UpdateStudent_InvalidStudentID()
    {
      // Arrange
      StudentUpdateRequest studentUpdateRequest = new StudentUpdateRequest() { StudentID = Guid.NewGuid()};
      // Assert
      Assert.Throws<ArgumentException>(() => _studentService.UpdateStudent(studentUpdateRequest));
    }
    [Fact]
    public void UpdateStudent_StudentNameIsNull()
    {
      // Arrange
      ClassroomAddRequest classroomAddRequest = new ClassroomAddRequest() { ClassName = "DSA" };
      ClassroomResponse classroomResponseFromAdd = _classroomService.AddClassroom(classroomAddRequest);
      StudentAddRequest studentAddRequest = new StudentAddRequest() { 
        StudentName = "John",
        Email = "AA@gmail.com",
        Gender = GenderOptions.Male,
        ClassID = classroomResponseFromAdd.ClassID,
      };
      StudentResponse studentResponseFromAdd = _studentService.AddStudent(studentAddRequest);
      StudentUpdateRequest studentUpdateRequest = studentResponseFromAdd.ToStudentUpdateRequest();
      studentUpdateRequest.StudentName = null;
      // Assert
      Assert.Throws<ArgumentException>(() => _studentService.UpdateStudent(studentUpdateRequest));
    }
    [Fact]
    public void UpdateStudent_StudentFullDetailsUpdation()
    {
      // Arrange
      ClassroomAddRequest classroomAddRequest = new ClassroomAddRequest() { ClassName = "DSA" };
      ClassroomResponse classroomResponseFromAdd = _classroomService.AddClassroom(classroomAddRequest);
      StudentAddRequest studentAddRequest = new StudentAddRequest()
      {
        StudentName = "John",
        ClassID = classroomResponseFromAdd.ClassID,
        Address = "A",
        Email = "A@gmail.com",
        Gender = GenderOptions.Male,
        DateOfBirth = DateTime.Parse("2000-01-01"),
        IsNewCommer = true,
      };
      StudentResponse studentResponseFromAdd = _studentService.AddStudent(studentAddRequest);
      StudentUpdateRequest studentUpdateRequest = studentResponseFromAdd.ToStudentUpdateRequest();
      studentUpdateRequest.StudentName = "B";
      studentUpdateRequest.Email = "B@gmail.com";
      // Act
      StudentResponse studentResponseFromUpdate = _studentService.UpdateStudent(studentUpdateRequest);
      StudentResponse? studentResponseFromGet = _studentService.GetStudentByStudentID(studentUpdateRequest.StudentID);
      // Assert
      Assert.Equal(studentResponseFromUpdate, studentResponseFromGet);
    }
    #endregion
    #region DeleteStudent
    [Fact]
    public void DeleteStudent_InvalidStudentID()
    {
      // Arrange
      ClassroomAddRequest classroomAddRequest = new ClassroomAddRequest()
      {
        ClassName = "DSA",
      };
      ClassroomResponse classroomResponseFromAdd = _classroomService.AddClassroom(classroomAddRequest);

      StudentAddRequest? studentAddRequest = new StudentAddRequest()
      {
        StudentName = "Example name",
        Address = "Example address",
        Email = "person@example.com",
        ClassID = classroomResponseFromAdd.ClassID,
        Gender = GenderOptions.Male,
        DateOfBirth = DateTime.Parse("2000-01-01"),
        IsNewCommer = true,
      };
      _studentService.AddStudent(studentAddRequest);
      // Act
      bool isDeleted = _studentService.DeleteStudent(Guid.NewGuid());
      // Assert
      Assert.False(isDeleted);
    }
    [Fact]
    public void DeleteStudent_ValidStudentID()
    {
      // Arrange
      ClassroomAddRequest classroomAddRequest = new ClassroomAddRequest()
      {
        ClassName = "DSA",
      };
      ClassroomResponse classroomResponseFromAdd = _classroomService.AddClassroom(classroomAddRequest);

      StudentAddRequest? studentAddRequest = new StudentAddRequest()
      {
        StudentName = "Example name",
        Address = "Example address",
        Email = "person@example.com",
        ClassID = classroomResponseFromAdd.ClassID,
        Gender = GenderOptions.Male,
        DateOfBirth = DateTime.Parse("2000-01-01"),
        IsNewCommer = true,
      };
      StudentResponse studentFromAdd = _studentService.AddStudent(studentAddRequest);
      // Act
      bool isDeleted = _studentService.DeleteStudent(studentFromAdd.StudentID);
      // Assert
      Assert.True(isDeleted);
    }
    #endregion
  }
}
