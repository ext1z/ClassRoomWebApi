using ClassRoomWebApi.ViewModels;

namespace ClassRoomWebApi.Models;

public class CourseDto
{
    public Guid Id { get; set; }
    public string? CourseTitle { get; set; }
    public string? Key { get; set; }

    public List<UserInfoDto>? Students { get; set; }

}
