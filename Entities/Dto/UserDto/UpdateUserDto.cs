using System;
using static Helpers.Enums;

namespace Fitness.Entities.Dto
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string  NewPassword { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
