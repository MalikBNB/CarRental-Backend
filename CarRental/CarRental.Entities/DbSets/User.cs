using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Entities.DbSets
{
    public class User : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}".ToUpper();

        public DateTime DateOfBirth { get; set; }

        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        public byte Status { get; set; } = 1;

        public byte[]? Picture { get; set; }

        public bool IsCustomer { get; set; }

        [MaxLength(100)]
        public string DriverLicenseNumber { get; set; } = string.Empty;

       
    }
}
