using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace Entities
{
  public class Student
  {
    public Guid StudentID { get; set; }
    public string? StudentName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public Guid? ClassID { get; set; }
    public string? Address { get; set; }
    public bool IsNewCommer { get; set; }
  }
}
