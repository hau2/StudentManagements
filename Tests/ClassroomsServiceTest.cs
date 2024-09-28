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
      _classroomService = new ClassroomsService();
    }

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

      // Assert
      Assert.True(response.ClassID != Guid.Empty);
    }
  }
}
