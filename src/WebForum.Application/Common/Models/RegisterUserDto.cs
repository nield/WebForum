namespace WebForum.Application.Common.Models;

public class RegisterUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string RoleName { get; set; }
}
