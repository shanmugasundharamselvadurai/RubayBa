using System;
using System.Collections.Generic;
using System.Text;

namespace Walkland.DTOs.Authentication
{
    public class GuestUserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Code { get; set; }
    }
}
