
using System.ComponentModel.DataAnnotations;
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
        
        [MaxLength(10)]
        public string Gendor { get; set; } = string.Empty;

        public byte Status { get; set; } = 1;

        public byte[]? Picture { get; set; }

        [MaxLength(100)]
        public string DriverLicenseNumber { get; set; } = string.Empty;

        public DateTime Created { get; set;}

        public DateTime Modified { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }

       
    }
}
