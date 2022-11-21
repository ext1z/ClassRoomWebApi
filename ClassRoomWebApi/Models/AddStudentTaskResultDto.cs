using ClassRoomWebApi.Entities;

namespace ClassRoomWebApi.Models;

public class AddStudentTaskResultDto
{
    public string? Description { get; set; }
    public EStudentTaskStatus Status { get; set; }
}
