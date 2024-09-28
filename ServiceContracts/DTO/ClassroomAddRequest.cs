using Entities;
namespace ServiceContracts.DTO
{
  public class ClassroomAddRequest
  {
    public string? ClassName { get; set; }

    public Classroom ToClassroom()
    {
      return new Classroom()
      {
        ClassName = ClassName,
      };
    }
  }
}
