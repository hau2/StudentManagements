using Entities;
namespace ServiceContracts.DTO
{
  public class ClassroomResponse
  {
    public Guid ClassID {  get; set; }
    public string? ClassName { get; set;}
    public override bool Equals(object? obj)
    {
      if (obj == null) return false;
      if (obj.GetType() != typeof(ClassroomResponse)) return false;

      ClassroomResponse classroom_to_compare = (ClassroomResponse)obj;
      return this.ClassID == classroom_to_compare.ClassID && this.ClassName == classroom_to_compare.ClassName;
    }
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
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
