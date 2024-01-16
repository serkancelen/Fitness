using System;
using static Helpers.Enums;

namespace Fitness.Entities.Dto.UserDto
{
    public class AddUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
