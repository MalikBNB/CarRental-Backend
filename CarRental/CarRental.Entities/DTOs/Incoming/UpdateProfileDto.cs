using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities.Interfaces;

namespace CarRental.Entities.DTOs.Incoming
{
    public class UpdateProfileDto
    {
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, StringLength(250)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Gendor { get; set; } = string.Empty;

        public byte[]? Picture { get; set; }

        public string DriverLicenseNumber { get; set; } = string.Empty;
    }
}
