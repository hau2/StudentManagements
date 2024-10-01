﻿using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services
{
  public class StudentsService : IStudentService
  {
    private readonly List<Student> _students;
    private readonly IClassroomService _classroomService;
    public StudentsService()
    {
      _students = new List<Student>();
      _classroomService = new ClassroomsService();
    }
    private StudentResponse ConvertStudentToStudentResponse(Student student)
    {
      StudentResponse studentResponse = student.ToStudentResponse();
      studentResponse.Classroom = _classroomService.GetClassroomByClassID(student.ClassID)?.ClassName;
      return studentResponse;
    }
    public StudentResponse AddStudent(StudentAddRequest? studentAddRequest)
    {
      if (studentAddRequest == null)
      {
        throw new ArgumentNullException(nameof(studentAddRequest));
      }
      // Model validation
      ValidationHelper.ModelValidation(studentAddRequest);
      // Validation StudentName
      if (string.IsNullOrEmpty(studentAddRequest.StudentName))
      {
        throw new ArgumentException("StudentName can't be blank");
      }
      // Convert studentAddRequest to Student type
      Student student = studentAddRequest.ToStudent();
      // Generate StudentID
      student.StudentID = Guid.NewGuid();
      // Add Student to the list
      _students.Add(student);
      // Convert the Student object into StudentResponse type
      return ConvertStudentToStudentResponse(student);
    }

    public List<StudentResponse> GetAllStudents()
    {
      return _students.Select(s => s.ToStudentResponse()).ToList();
    }

    public StudentResponse? GetStudentByStudentID(Guid? studentID)
    {
      if (studentID == null) return null;
      Student? student = _students.FirstOrDefault(s => s.StudentID == studentID);
      if (student == null) return null;
      return ConvertStudentToStudentResponse(student);
    }

    public List<StudentResponse> GetFilteredStudents(string searchBy, string? searchString)
    {
      List<StudentResponse> allStudents = GetAllStudents();
      List<StudentResponse> matchingStudents = allStudents;

      if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
      {
        return matchingStudents;
      }

      StringComparison comparison = StringComparison.OrdinalIgnoreCase;
      switch (searchBy)
      {
        case nameof(Student.StudentName):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.StudentName) ? s.StudentName.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(Student.Email):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Email) ? s.Email.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(Student.DateOfBirth):
          matchingStudents = allStudents.Where(s => (s.DateOfBirth != null) ? s.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, comparison) : true).ToList();
          break;
        case nameof(Student.Gender):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Gender) ? s.Gender.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(Student.ClassID):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Classroom) ? s.Classroom.Contains(searchString, comparison) : true)).ToList();
          break;
        case nameof(Student.Address):
          matchingStudents = allStudents.Where(s => (!string.IsNullOrEmpty(s.Address) ? s.Address.Contains(searchString, comparison) : true)).ToList();
          break;
        default: matchingStudents = allStudents; break;
      }
      return matchingStudents;
    }

    public List<StudentResponse> GetSortedStudents(List<StudentResponse> allStudents, string sortBy, SortOrderOptions sortOrder)
    {
      if (string.IsNullOrEmpty(sortBy)) return allStudents;
      List<StudentResponse> sortedStudents = (sortBy, sortOrder)
      switch
      {
        (nameof(StudentResponse.StudentName), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.StudentName, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.StudentName), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.StudentName, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Email), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.Email, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Email), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.Email, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.DateOfBirth), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.DateOfBirth).ToList(),
        (nameof(StudentResponse.DateOfBirth), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.DateOfBirth).ToList(),
        (nameof(StudentResponse.Age), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.Age).ToList(),
        (nameof(StudentResponse.Age), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.Age).ToList(),
        (nameof(StudentResponse.Gender), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Gender), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Classroom), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.Classroom, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Classroom), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.Classroom, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Address), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.Address, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.Address), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.Address, StringComparer.OrdinalIgnoreCase).ToList(),
        (nameof(StudentResponse.IsNewCommer), SortOrderOptions.ASC) => allStudents.OrderBy(s => s.IsNewCommer).ToList(),
        (nameof(StudentResponse.IsNewCommer), SortOrderOptions.DESC) => allStudents.OrderByDescending(s => s.IsNewCommer).ToList(),
        _ => allStudents
      };
      return sortedStudents;
    }

    public StudentResponse UpdateStudent(StudentUpdateRequest studentUpdateRequest)
    {
      if(studentUpdateRequest == null) throw new ArgumentNullException(nameof(studentUpdateRequest));
      // validation
      ValidationHelper.ModelValidation(studentUpdateRequest);
      // get matching student object to update
      Student? matchingStudent = _students.FirstOrDefault(s => s.StudentID == studentUpdateRequest.StudentID);
      if (matchingStudent == null) throw new ArgumentException("Given student id doesn't exist");
      // update all details
      matchingStudent.StudentName = studentUpdateRequest.StudentName;
      matchingStudent.Email = studentUpdateRequest.Email;
      matchingStudent.Address = studentUpdateRequest.Address;
      matchingStudent.Gender = studentUpdateRequest.Gender.ToString();
      matchingStudent.DateOfBirth = studentUpdateRequest.DateOfBirth;
      matchingStudent.ClassID = studentUpdateRequest.ClassID;
      matchingStudent.IsNewCommer = studentUpdateRequest.IsNewCommer;

      return matchingStudent.ToStudentResponse();
    }

    public bool DeleteStudent(Guid? studentID)
    {
      if(studentID == null) throw new ArgumentException(nameof(studentID));
      Student? student = _students.FirstOrDefault(s => s.StudentID == studentID);
      if (student == null) return false;
      _students.RemoveAll(s => s.StudentID == studentID);
      return true;
    }
  }
}
