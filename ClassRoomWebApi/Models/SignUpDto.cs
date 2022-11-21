using System.ComponentModel.DataAnnotations;

namespace ClassRoomWebApi.Models;

public class SignUpDto
{
    [Required(ErrorMessage ="Malumot bolishi shart.")]
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required(ErrorMessage ="Parol bolishi shart")]
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}
