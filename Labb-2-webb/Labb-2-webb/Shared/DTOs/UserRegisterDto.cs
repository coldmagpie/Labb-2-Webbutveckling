using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
