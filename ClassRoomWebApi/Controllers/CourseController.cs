using ClassRoomWebApi.Context;
using ClassRoomWebApi.Entities;
using ClassRoomWebApi.Mappers;
using ClassRoomWebApi.Models;
using ClassRoomWebApi.ViewModels;
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
public partial class CourseController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityStudent> _userManager;

    public CourseController(AppDbContext context, UserManager<IdentityStudent> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    

    [HttpPost]
    public async Task<IActionResult> AddCourse([FromBody] AddCourseDto addCourse)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var student = await _userManager.GetUserAsync(User);
        var course = new Course()
        {
            CourseTitle = addCourse.CourseTitle,
            Key = Guid.NewGuid().ToString("N"),
            Students = new List<StudentCourse>()
            {
                new StudentCourse()
                {
                    StudentId = student.Id,
                    IsAdmin = true
                }
            }
        };

        await _context.AddAsync(course);
        await _context.SaveChangesAsync();

        course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == course.Id);
        var courseDto = course?.ToDto();

        return Ok(courseDto);

    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetCourseById(Guid courseId)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);

        if (course is null)
            return NotFound();

        return Ok(course.ToDto());

    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _context.Courses.ToListAsync();
        if (courses is null)
            return NotFound();

        List<CourseDto> coursesDto = courses.Select(c => c.ToDto()).ToList() ;

        return Ok(coursesDto);
    }

    [HttpPut("{courseid}")]
    public async Task<IActionResult> UpdateCourse(Guid courseId, [FromBody] UpdateCourseDto updateCourse)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if(!await _context.Courses.AnyAsync(c => c.Id == courseId))
            return NotFound();

        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            return NotFound();

        var student = await _userManager.GetUserAsync(User);
        if (course.Students?.Any(uc => uc.StudentId == student.Id && uc.IsAdmin) != true)
            return BadRequest();


        course.CourseTitle = updateCourse.CourseTitle;
        await _context.SaveChangesAsync();

        return Ok(course.ToDto());
    }

    [HttpDelete("{courseId}")]
    public async Task<IActionResult> DeleteCourse(Guid courseId)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            return NotFound();

        var student = await _userManager.GetUserAsync(User);
        if (course.Students?.Any(uc => uc.StudentId == student.Id && uc.IsAdmin) != true)
            return BadRequest();
         

        _context.Remove(course);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{courseId}/join")]
    public async Task<IActionResult> JoinToCourse(Guid courseId, JoinToCourseDto joinToCourse)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            return NotFound();

        var student = await _userManager.GetUserAsync(User);
        if (course.Students?.Any(uc => uc.StudentId == student.Id ) == true)
            return BadRequest();

        _context.StudentCourses.Add(new StudentCourse
        {
            StudentId = student.Id,
            CourseId = course.Id,
            IsAdmin = false
        });

        await _context.SaveChangesAsync();

        return Ok();
    }
}
