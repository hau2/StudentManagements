using ServiceContracts.DTO;

namespace ServiceContracts
{
  public interface IClassroomService
  {
    ClassroomResponse AddClassroom(ClassroomAddRequest? classroomAddRequest);
    List<ClassroomResponse> GetAllClassrooms();
    ClassroomResponse? GetClassroomByClassID(Guid? classID);
  }
}
