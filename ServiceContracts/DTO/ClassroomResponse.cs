using Entities;
namespace ServiceContracts.DTO
{
  public class ClassroomResponse
  {
    public Guid ClassID {  get; set; }
    public string? ClassName { get; set;}
  }

  public static class CourseExtension
  {
    public static ClassroomResponse ToClassroomResponse(this Classroom classroom)
    {
      return new ClassroomResponse()
      {
        ClassID = classroom.ClassID,
        ClassName = classroom.ClassName
      };
    }
  }
}
