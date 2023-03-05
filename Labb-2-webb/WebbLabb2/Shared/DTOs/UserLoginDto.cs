using System.ComponentModel.DataAnnotations;

namespace WebbLabb2.Shared.DTOs;

public class UserLoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Invalid password!")]
    public string Password { get; set; }
}