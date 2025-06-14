﻿using System;

namespace SitarLib.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // "Admin", "Staff"
        public bool IsActive { get; set; }
        public DateTime LastLogin { get; set; }
    }
}