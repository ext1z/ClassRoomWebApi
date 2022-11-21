using ClassRoomWebApi.Mappers;
using ClassRoomWebApi.Models;
using ClassRoomWebApi.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRoomWebApi.Controllers;


public partial class CourseController
{

    [HttpPost("{courseId}/task")]
    public async Task<IActionResult> AddTask(Guid courseId, [FromBody] AddTaskDto addTaskDto)
    {
        if(!ModelState.IsValid)
            return BadRequest();

        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            return NotFound();

        var student = await _userManager.GetUserAsync(User);
        if (course.Students?.Any(uc => uc.StudentId == student.Id && uc.IsAdmin) != true)
            return BadRequest();


        var task = addTaskDto.Adapt<ClassRoomWebApi.Entities.Task>();
        task.CreatedDate = DateTime.Now;
        task.CourseId = courseId;

        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return Ok(task.Adapt<TaskInfoDto>());
    }

    [HttpGet("{courseId}/tasks")]
    public async Task<IActionResult> GetTasks(Guid courseId)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            return NotFound();

        var task = course.Tasks?.Select(task => task.Adapt<TaskInfoDto>()).ToList();

        return Ok(task);
    }
     
    [HttpGet("{courseId}/tasks/{taskId}")] // mb change
    public async Task<IActionResult> GetTaskById(Guid courseId, Guid taskId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.CourseId == courseId);
        if (task is null)
            return NotFound();

        return Ok(task.Adapt<TaskInfoDto>());
    }

    [HttpPut("{courseId}/tasks/{taskId}")]
    public async Task<IActionResult> UpdateTask(Guid courseId, Guid taskId, UpdateTaskDto updateTaskDto)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.CourseId == courseId);
        if (task is null)
            return NotFound();

        task.SetValues(updateTaskDto);
        await _context.SaveChangesAsync();

        return Ok(task.Adapt<TaskInfoDto>());
    }

    [HttpDelete("{courseid}/tasks/{taskId}")]
    public async Task<IActionResult> DeleteTask(Guid courseId, Guid taskId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.CourseId == courseId);
        if (task is null)
            return NotFound();

        _context.Remove(task);
        await _context.SaveChangesAsync();

        return Ok();
    }

}
