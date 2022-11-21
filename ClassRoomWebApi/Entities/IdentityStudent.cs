using Microsoft.AspNetCore.Identity;

namespace ClassRoomWebApi.Entities;

public class IdentityStudent : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public virtual List<StudentCourse>? Courses { get; set; }
    public virtual List<StudentTask>? StudentTasks { get; set; }
}
