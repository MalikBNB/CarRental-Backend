using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Authentication.Models.DTOs.Incoming;
public class RevokeToken
{
    public string? Token { get; set; }
}
