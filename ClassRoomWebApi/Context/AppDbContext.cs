using ClassRoomWebApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClassRoomWebApi.Context;

public class AppDbContext : IdentityDbContext<IdentityStudent, IdentityRoles, Guid>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<Entities.Task> Tasks { get; set; }
    public DbSet<StudentTask> StudentTasks { get; set; }
}
