using Entities;
using ServiceContracts.Enums;
namespace ServiceContracts.DTO
{
  public class StudentAddRequest
  {
    public string? StudentName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public GenderOptions? Gender { get; set; }
    public Guid? ClassID { get; set; }
    public string? Address { get; set; }
    public bool IsNewCommer { get; set; }
    public Student ToStudent()
    {
      return new Student() { 
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
