using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Authentication.Models.DTOs.Incoming
{
    public class RegistrationRequestDto
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        [Required, StringLength(128)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(256)]
        public string Password { get; set; } = string.Empty;
    }
}
