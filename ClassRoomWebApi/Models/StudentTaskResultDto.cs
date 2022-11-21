using ClassRoomWebApi.Entities;
using ClassRoomWebApi.ViewModels;

namespace ClassRoomWebApi.Models;

public class StudentTaskResultDto : TaskInfoDto
{
    public StudentTaskResult? StudentResult { get; set; }
}

public class StudentTaskResult
{
    public string? Description { get; set; }
    public EStudentTaskStatus Status { get; set; }
}