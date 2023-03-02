using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UserLoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Invalid password!")]
    public string Password { get; set; }
}

