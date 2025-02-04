using CarRental.Authentication.Models;
using CarRental.Authentication.Models.DTOs.Incoming;

namespace CarRental.Authentication.Services;

public interface IAuthService
{
    Task<AuthModel> RegisterAsync(RegistrationRequestDto registrationDto);
    Task<AuthModel> LoginAsync(LoginRequestDto loginDto);
    Task<string> AddRoleAsync(AddRoleDto roleDto);
}
