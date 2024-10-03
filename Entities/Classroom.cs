using System.ComponentModel.DataAnnotations;

namespace Entities
{
  public class Classroom
  {
    [Key]
    public Guid ClassID { get; set; }
    public string? ClassName { get; set; }
    public string? TeacherName { get; set; }
  }
}
