﻿namespace BibliotecaCrud.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

    }
}
