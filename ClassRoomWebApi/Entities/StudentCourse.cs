using System.ComponentModel.DataAnnotations.Schema;

namespace ClassRoomWebApi.Entities;

public class StudentCourse
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }
    [ForeignKey(nameof(StudentId))]
    public virtual IdentityStudent? Student { get; set; }

    public Guid CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public virtual Course? Course { get; set; }

    public bool IsAdmin { get; set; }

}
