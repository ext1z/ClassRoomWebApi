using System.ComponentModel.DataAnnotations;

namespace ClassRoomWebApi.Models;

public class UpdateCourseDto
{
    [Required]
    public string CourseTitle { get; set; }
}
