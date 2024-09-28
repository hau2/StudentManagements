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
    public static ClassroomResponse ToCourseResponse(this Classroom course)
    {
      return new ClassroomResponse()
      {
        ClassID = course.CourseID,
        ClassName = course.ClassName
      };
    }
  }
}
