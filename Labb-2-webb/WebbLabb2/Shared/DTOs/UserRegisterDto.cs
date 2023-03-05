using System.ComponentModel.DataAnnotations;

namespace WebbLabb2.Shared.DTOs;

public class UserRegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Adress { get; set; }
    [Required, StringLength(100, MinimumLength = 8)]
    public string Password { get; set; }
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}