﻿
using System.ComponentModel.DataAnnotations;

namespace WebbLabb2.Shared.DTOs
{
    public class UserPasswordDto
    {
        public int Id { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
