using ClassRoomWebApi.Context;
using ClassRoomWebApi.Entities;
using ClassRoomWebApi.Mappers;
using ClassRoomWebApi.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRoomWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityStudent> _userManager;

    public ProfileController(AppDbContext context, UserManager<IdentityStudent> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("courses")]
    public async Task<IActionResult> GetCourses()
    {
        var student = await _userManager.GetUserAsync(User);
        List<CourseDto> coursesDto = student.Courses?.Select(userCourse => userCourse.Course.ToDto()).ToList();

        return Ok(coursesDto);
    }

    [HttpGet("courses/{courseId}/tasks")]
    public async Task<IActionResult> GetStudentTasks(Guid courseId)
    {
        var student = await _userManager.GetUserAsync(User);

        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);

        if (course?.Tasks is null)
            return NotFound();

        var tasks = course.Tasks;
        var studentTasks = new List<StudentTaskResultDto>();


        foreach (var task in tasks)
        {
            var result = task.Adapt<StudentTaskResultDto>();
            var studentResultEntity =  task.StudentTasks?.FirstOrDefault(s => s.StudentId == student.Id);

            result.StudentResult = studentResultEntity == null ? null : new StudentTaskResult()
            {
                Status = studentResultEntity.Status,
                Description = studentResultEntity.Description
            };

            studentTasks.Add(result);
        }

        return Ok(studentTasks);
    }

    [HttpPost("courses/{courseId}/tasks/{taskId}")]
    public async Task<IActionResult> AddStudentTaskResult(Guid courseId, Guid taskId, [FromBody] AddStudentTaskResultDto taskResultDto)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.CourseId == courseId);
        if (task is null)
            return NotFound();

        var student = await _userManager.GetUserAsync(User);

        var studentTaskResult = await _context.StudentTasks
            .FirstOrDefaultAsync(ut => ut.StudentId == student.Id && ut.TaskId == taskId);

        if (studentTaskResult is null)
        {
            studentTaskResult = new StudentTask()
            {
                StudentId = student.Id,
                TaskId = taskId
            };

            await _context.StudentTasks.AddAsync(studentTaskResult);
        }

        studentTaskResult.Description = taskResultDto.Description;
        studentTaskResult.Status = taskResultDto.Status;

        await _context.SaveChangesAsync();

        return Ok("Thanks for answer");
    }



}
