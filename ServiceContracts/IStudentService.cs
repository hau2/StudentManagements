using ServiceContracts.DTO;
namespace ServiceContracts
{
  public interface IStudentService
  {
    StudentResponse AddStudent(StudentAddRequest? student);
    List<StudentResponse> GetAllStudents();
  }
}
