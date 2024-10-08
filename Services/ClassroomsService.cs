﻿using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
  public class ClassroomsService : IClassroomService
  {
    // private field
    private readonly List<Classroom> _classrooms;

    // constructor
    public ClassroomsService(bool initialize = true)
    {
      _classrooms = new List<Classroom>();
      if(initialize)
      {
        _classrooms.AddRange(new List<Classroom>()
        {
					new Classroom() { ClassID = Guid.Parse("8E7E3442-6B8D-4C25-9B2E-054AFF4BCBBE"), ClassName = "DSA" },
				  new Classroom() { ClassID = Guid.Parse("ED59FCD0-8DA4-4AD1-952E-86B40DF4E2ED"), ClassName = "Database" }
				});
			}
		}
    public ClassroomResponse AddClassroom(ClassroomAddRequest classroomAddRequest)
    {
      // Validation: classroomAddRequest paramater can't be null
      if(classroomAddRequest == null)
      {
        throw new ArgumentNullException(nameof(classroomAddRequest));
      }
      // Validatiton: ClassName can't be null
      if(classroomAddRequest.ClassName == null)
      {
        throw new ArgumentException(nameof(classroomAddRequest));
      }
      // Validation: ClassName can't be duplicate
      if(_classrooms.Where(temp => temp.ClassName == classroomAddRequest?.ClassName).Count() > 0)
      {
        throw new ArgumentException("Given class name already exists");
      }
      // Convert object from ClassroomAddRequest to Classroom type
      Classroom classroom = classroomAddRequest.ToClassroom();

      // Generate ClassID
      classroom.ClassID = Guid.NewGuid();

      // Add Classroom object into _classrooms
      _classrooms.Add(classroom);

      return classroom.ToClassroomResponse();
    }

    public List<ClassroomResponse> GetAllClassrooms()
    {
      return _classrooms.Select(c => c.ToClassroomResponse()).ToList();
    }

    public ClassroomResponse? GetClassroomByClassID(Guid? classID)
    {
      if (classID == null) return null;
      Classroom? classroom_response_form_list = _classrooms.FirstOrDefault(c => c.ClassID == classID);

      if (classroom_response_form_list == null) return null;

      return classroom_response_form_list.ToClassroomResponse();
    }
  }
}
