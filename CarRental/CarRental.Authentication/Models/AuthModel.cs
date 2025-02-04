using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Authentication.Models;
public class AuthModel
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsAuthenticated { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresOn { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

}
