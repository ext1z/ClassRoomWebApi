using ClassRoomWebApi.Models;

namespace ClassRoomWebApi.Mappers;

public static class TaskMapper
{

    public static void SetValues(this ClassRoomWebApi.Entities.Task task, UpdateTaskDto updateTaskDto)
    {
        task.Title = updateTaskDto.Title;
        task.Description = updateTaskDto.Description;
        task.MaxScore = updateTaskDto.MaxScore;
        task.StartDate = updateTaskDto.StartDate;
        task.EndDate = updateTaskDto.EndDate;
        task.Status = updateTaskDto.Status;

    }
}
