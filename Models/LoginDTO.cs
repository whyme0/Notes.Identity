﻿using System.ComponentModel.DataAnnotations;

namespace Notes.Identity.Models
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "This field is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

    }
}
