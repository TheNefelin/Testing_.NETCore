﻿using System.ComponentModel.DataAnnotations;

namespace BibliotecaDeClases.DTOs
{
    public class AuthRegisterDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string PasswordConfirmed { get; set; } = string.Empty;
    }
}
