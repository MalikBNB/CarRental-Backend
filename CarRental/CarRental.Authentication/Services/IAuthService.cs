using CarRental.Authentication.Models.DTOs.Incoming;
using CarRental.Authentication.Models.DTOs.Outgoing;

namespace CarRental.Authentication.Services;

public interface IAuthService
{
    Task<AuthModel> RegisterAsync(RegistrationRequestDto registrationDto);
    Task<AuthModel> LoginAsync(LoginRequestDto loginDto);
    Task<string> AddRoleAsync(AddRoleDto roleDto);
}
