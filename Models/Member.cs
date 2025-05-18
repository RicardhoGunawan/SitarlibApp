using System;

namespace SitarLib.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string Class { get; set; } // Field kelas baru

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime MembershipExpiry { get; set; }
        public bool IsActive { get; set; }
    }
}