using ServiceContracts.DTO;
using ServiceContracts.Enums;
namespace ServiceContracts
{
  public interface IStudentService
  {
    StudentResponse AddStudent(StudentAddRequest? student);
    List<StudentResponse> GetAllStudents();
    StudentResponse? GetStudentByStudentID(Guid? studentID);
    List<StudentResponse> GetFilteredStudents(string searchBy, string? serachString);
    List<StudentResponse> GetSortedStudents(List<StudentResponse> allStudents,string sortBy, SortOrderOptions sortOrder);
    StudentResponse UpdateStudent(StudentUpdateRequest studentUpdateRequest);
  }
}
