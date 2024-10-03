using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace Entities
{
  public class Student
  {
    [Key]
    public Guid StudentID { get; set; }
    [StringLength(40)] // nvarchar(40)
    public string? StudentName { get; set; }
		[StringLength(40)]
		public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
		[StringLength(10)]
		public string? Gender { get; set; }
    public Guid? ClassID { get; set; }
		[StringLength(200)]
		public string? Address { get; set; }
    // bit
    public bool IsNewCommer { get; set; }
  }
}
