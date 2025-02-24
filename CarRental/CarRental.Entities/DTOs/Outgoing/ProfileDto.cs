using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities.Interfaces;

namespace CarRental.Entities.DTOs.Outgoing
{
    public class ProfileDto : IProfileDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".ToUpper();
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gendor { get; set; } = string.Empty;
        public byte[]? Picture { get; set; }
        public string DriverLicenseNumber { get; set; } = string.Empty;
    }
}
