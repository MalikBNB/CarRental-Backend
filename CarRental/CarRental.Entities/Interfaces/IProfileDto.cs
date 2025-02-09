using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.Interfaces
{
    public interface IProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}".ToUpper();
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gendor { get; set; }
        public byte[]? Picture { get; set; }
        public string DriverLicenseNumber { get; set; }
    }
}
