using ClassRoomWebApi.Entities;
using ClassRoomWebApi.Models;
using ClassRoomWebApi.ViewModels;
using Mapster;

namespace ClassRoomWebApi.Mappers;

public static class CourseMapper
{
    public static CourseDto ToDto(this Course course)
    {
        return new CourseDto()
        {
            Id = course.Id,
            CourseTitle = course.CourseTitle,
            Key = course.Key,
            Students = course.Students?.Select(studentCourse => studentCourse.Student?.Adapt<UserInfoDto>()).ToList()
        };
    }
}
