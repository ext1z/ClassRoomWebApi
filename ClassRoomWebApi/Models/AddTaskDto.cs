using ClassRoomWebApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClassRoomWebApi.Models;

public class AddTaskDto
{
    [Required]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MaxScore { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ETaskStatus Status { get; set; }

}
