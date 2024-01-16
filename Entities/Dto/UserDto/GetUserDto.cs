using Fitness.Entities.Models;
using System;
using static Helpers.Enums;

namespace Fitness.Entities.Dto
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public List<ProgressLog> ProgressLogs { get; set; }

    }
}
