using ClassRoomWebApi.Entities;

namespace ClassRoomWebApi.ViewModels;

public class TaskInfoDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MaxScore { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ETaskStatus Status { get; set; }
}
