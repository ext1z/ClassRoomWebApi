namespace ClassRoomWebApi.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string? CourseTitle { get; set; }
    public string? Key { get; set; }

    public virtual List<StudentCourse>? Students { get; set; }
    public virtual List<Task>? Tasks { get; set; }
}
