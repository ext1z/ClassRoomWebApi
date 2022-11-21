using System.ComponentModel.DataAnnotations.Schema;

namespace ClassRoomWebApi.Entities;

public class StudentTask
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }
    [ForeignKey(nameof(StudentId))]
    public virtual IdentityStudent? Student { get; set; }
    public Guid TaskId { get; set; }
    [ForeignKey(nameof(TaskId))]
    public virtual Task? Task { get; set; }

    public EStudentTaskStatus Status { get; set; }
    public string? Description { get; set; }

}
