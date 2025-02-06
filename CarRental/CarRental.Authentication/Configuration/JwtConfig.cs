using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Authentication.Configuration
{
    public class JwtConfig
    {
        public static string SectionName { get; set; } = "JwtConfig";
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        //public double DurationInDays { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
