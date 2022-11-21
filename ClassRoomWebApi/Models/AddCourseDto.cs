using System.ComponentModel.DataAnnotations;

namespace ClassRoomWebApi.Models;

public class AddCourseDto
{
    [Required]
    public string CourseTitle { get; set; }

}
