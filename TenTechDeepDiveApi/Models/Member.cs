using System;
using System.ComponentModel.DataAnnotations;

namespace TenTechDeepDiveApi.Models
{
    public class Member
    {
        [Key] // Primary Key
        public int Id { get; set; } // Or generate a GUID if you prefer string IDs
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BookingCount { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
