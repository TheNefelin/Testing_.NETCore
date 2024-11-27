﻿using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models.DTOs
{
    public class RegisterRequestDTO
    {
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }

        [MinLength(6)]
        [MaxLength(25)]
        public required string Password1 { get; set; }
        
        [MinLength(6)]
        [MaxLength(25)]
        public required string Password2 { get; set; }
    }
}