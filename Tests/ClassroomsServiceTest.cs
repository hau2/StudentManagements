using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace Tests
{
  public class ClassroomsServiceTest
  {
    private readonly IClassroomService _classroomService;

    public ClassroomsServiceTest()
    {
      _classroomService = new ClassroomsService(false);
    }

    #region AddClassroom
    // When ClassroomAddRequest is null, it should throw ArgumentException
    [Fact]
    public void AddClassroom_NullClassroom()
    {
      // Arrange
      ClassroomAddRequest? request = null;

      // Assert
      Assert.Throws<ArgumentNullException>(() => _classroomService.AddClassroom(request));
    }
    // When the ClassName is null, it should throw ArgumentException
    [Fact]
    public void AddClassroom_NullClassNameIsNull()
    {
      // Arrange
      ClassroomAddRequest? request = new ClassroomAddRequest() { ClassName = null};

      // Assert
      Assert.Throws<ArgumentException>(() => _classroomService.AddClassroom(request));
    }
    // When the ClassName is dupplicate, it should thow ArgumentException
    [Fact]
    public void AddClassroom_DuplicateClassName()
    {
      // Arrange
      ClassroomAddRequest? request1 = new ClassroomAddRequest() { ClassName = "DSA" };
      ClassroomAddRequest? request2 = new ClassroomAddRequest() { ClassName = "DSA" };

      // Assert
      Assert.Throws<ArgumentException>(() =>
      {
        // Atc
        _classroomService.AddClassroom(request1);
        _classroomService.AddClassroom(request2);
      });
    }
    // When you supply proper class name, it should insert (add) the class to the existing list of classes
    [Fact]
    public void AddClassroom_ProperClassroomDetails()
    {
      // Arrange
      ClassroomAddRequest? request = new ClassroomAddRequest() { ClassName = "DSA" };

      // Atc
      ClassroomResponse response = _classroomService.AddClassroom(request);
      List<ClassroomResponse> classroms_from_GetAllClassrooms = _classroomService.GetAllClassrooms();

      // Assert
      Assert.True(response.ClassID != Guid.Empty);
      Assert.Contains(response, classroms_from_GetAllClassrooms);
    }
    #endregion
    #region GetAllClassrooms
    [Fact]
    public void GetAllClassrooms_Emptylist()
    {
      // Acts
      List<ClassroomResponse> actual_classroom_response_list = _classroomService.GetAllClassrooms();

      // Assert
      Assert.Empty(actual_classroom_response_list);
    }
    [Fact]
    public void GetAllClassrooms_AddFewClassrooms()
    {
      List<ClassroomAddRequest> classrooms_request_list = new List<ClassroomAddRequest>()
      {
        new ClassroomAddRequest() {ClassName = "DSA"},
        new ClassroomAddRequest() {ClassName = "Database"},
      };

      // Act
      List<ClassroomResponse> classrooms_list_from_add_classroom = new List<ClassroomResponse>();
      foreach(ClassroomAddRequest classroom_request in classrooms_request_list)
      {
        classrooms_list_from_add_classroom.Add(_classroomService.AddClassroom(classroom_request));
      }

      List<ClassroomResponse> actualClassroomResponseList = _classroomService.GetAllClassrooms();

      foreach(ClassroomResponse expected_classroom in classrooms_list_from_add_classroom)
      {
        Assert.Contains(expected_classroom, actualClassroomResponseList);
      }
    }
    #endregion
    #region GetClassroomByClassID
    [Fact]
    public void GetClassroomByClassID_NullClassID()
    {
      // Arrange
      Guid? classID = null;
      // Act
      ClassroomResponse? classroom_response_form_get_method = _classroomService.GetClassroomByClassID(classID);
      // Assert
      Assert.Null(classroom_response_form_get_method);
    }
    [Fact]
    public void GetClassroomByClassID_ValidClassID()
    {
      // Arrange
      ClassroomAddRequest? classroom_add_request = new ClassroomAddRequest()
      {
        ClassName = "DSA"
      };
      ClassroomResponse? classroom_response_from_add = _classroomService.AddClassroom(classroom_add_request);
      // Act
      ClassroomResponse? classroom_response_from_get = _classroomService.GetClassroomByClassID(classroom_response_from_add.ClassID);
      // Assert
      Assert.Equal(classroom_response_from_add, classroom_response_from_get);
    }
    #endregion
  }
}
