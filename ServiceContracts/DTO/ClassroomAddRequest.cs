using Entities;
namespace ServiceContracts.DTO
{
  public class ClassroomAddRequest
  {
    public string? ClassName { get; set; }

    public Classroom ToCourse()
    {
      return new Classroom()
      {
        ClassName = ClassName,
      };
    }
  }
}
