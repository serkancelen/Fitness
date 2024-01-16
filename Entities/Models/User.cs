using Microsoft.AspNetCore.Identity;
using static Helpers.Enums;

namespace Fitness.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public List<ProgressLog> ProgressLogs { get; set; }
        public Gender Gender { get ; set; }
        public DateTime Birthdate { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}