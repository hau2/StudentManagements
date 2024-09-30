using ServiceContracts.DTO;
namespace ServiceContracts
{
  public interface IStudentService
  {
    StudentResponse AddStudent(StudentAddRequest? student);
    List<StudentResponse> GetAllStudents();
    StudentResponse? GetStudentByStudentID(Guid? studentID);
    List<StudentResponse> GetFilteredStudents(string searchBy, string? serachString);
  }
}
