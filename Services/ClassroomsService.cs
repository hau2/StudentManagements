using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
  public class ClassroomsService : IClassroomService
  {
    // private field
    private readonly List<Classroom> _classrooms;

    // constructor
    public ClassroomsService()
    {
      _classrooms = new List<Classroom>();
    }
    public ClassroomResponse AddClassroom(ClassroomAddRequest classroomAddRequest)
    {
      // Validation: classroomAddRequest paramater can't be null
      if(classroomAddRequest == null)
      {
        throw new ArgumentNullException(nameof(classroomAddRequest));
      }
      // Validatiton: ClassName can't be null
      if(classroomAddRequest.ClassName == null)
      {
        throw new ArgumentException(nameof(classroomAddRequest));
      }
      // Validation: ClassName can't be duplicate
      if(_classrooms.Where(temp => temp.ClassName == classroomAddRequest?.ClassName).Count() > 0)
      {
        throw new ArgumentException("Given class name already exists");
      }
      // Convert object from ClassroomAddRequest to Classroom type
      Classroom classroom = classroomAddRequest.ToClassroom();

      // Generate ClassID
      classroom.ClassID = Guid.NewGuid();

      // Add Classroom object into _classrooms
      _classrooms.Add(classroom);

      return classroom.ToClassroomResponse();
    }

    public List<ClassroomResponse> GetAllClassrooms()
    {
      throw new NotImplementedException();
    }
  }
}
