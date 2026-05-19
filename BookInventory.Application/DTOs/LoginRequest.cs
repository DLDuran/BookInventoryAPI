using System;
using System.Collections.Generic;
using System.Text;

namespace BookInventory.Application.DTOs
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
