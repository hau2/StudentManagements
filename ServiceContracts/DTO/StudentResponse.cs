using Entities;

namespace ServiceContracts.DTO
{
  public class StudentResponse
  {
    public Guid StudentID { get; set; }
    public string? StudentName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public Guid? ClassID { get; set; }
    public string? Classroom { get; set; }
    public string? Address { get; set; }
    public bool IsNewCommer { get; set; }
    public double? Age { get; set; }
    public override bool Equals(object? obj)
    {
      if (obj == null) return false;
      if (obj.GetType() != typeof(StudentResponse)) return false;
      StudentResponse student = (StudentResponse)obj;
      return StudentID == student.StudentID
        && StudentName == student.StudentName
        && Email == student.Email
        && DateOfBirth == student.DateOfBirth
        && Gender == student.Gender
        && ClassID == student.ClassID
        && Address == student.Address
        && IsNewCommer == student.IsNewCommer
        && Age == student.Age;
    }
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
    public override string ToString()
    {
      return $"Student: {StudentID}-{StudentName}-{Email}-{DateOfBirth?.ToString("dd MMM yyyy")}-{Gender}-{ClassID}-{Classroom}-{IsNewCommer}";
    }
  }
  public static class StudentExtension
  {
    public static StudentResponse ToStudentResponse(this Student student)
    {
      return new StudentResponse()
      {
        StudentID = student.StudentID,
        StudentName = student.StudentName,
        Address = student.Address,
        ClassID = student.ClassID,
        Email = student.Email,
        Gender = student.Gender,
        IsNewCommer = student.IsNewCommer,
        DateOfBirth = student.DateOfBirth,
        Age = (student.DateOfBirth != null) ? Math.Round((DateTime.Now - student.DateOfBirth.Value).TotalDays / 365.25) : null
      };
    }
  }
}
