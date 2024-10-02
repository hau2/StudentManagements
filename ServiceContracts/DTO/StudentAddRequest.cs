using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;
namespace ServiceContracts.DTO
{
  public class StudentAddRequest
  {
    [Required(ErrorMessage = "Student Name can't be blank")]
    public string? StudentName { get; set; }
    [Required(ErrorMessage = "Student Email can't be blank")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
		[Required(ErrorMessage = "Gender can't be blank")]
		public GenderOptions? Gender { get; set; }
    [Required(ErrorMessage = "Please Select Class")]
    public Guid? ClassID { get; set; }
    public string? Address { get; set; }
    public bool IsNewCommer { get; set; }
    public Student ToStudent()
    {
      return new Student()
      {
        StudentName = StudentName,
        Address = Address,
        ClassID = ClassID,
        DateOfBirth = DateOfBirth,
        Email = Email,
        Gender = Gender.ToString(),
        IsNewCommer = IsNewCommer
      };
    }
  }
}
