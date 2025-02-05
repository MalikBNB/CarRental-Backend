using AutoMapper;
using CarRental.Authentication.Configuration;
using CarRental.Authentication.Models;
using CarRental.Authentication.Models.DTOs.Incoming;
using CarRental.Authentication.Models.DTOs.Outgoing;
using CarRental.Configuration.Messages;
using CarRental.Entities.DbSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace CarRental.Authentication.Services;

public class AuthService : IAuthService
{
    private readonly JwtConfig _jwtConfig;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public AuthService(UserManager<User> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, IMapper mapper, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _jwtConfig = optionsMonitor.CurrentValue;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<AuthModel> RegisterAsync(RegistrationRequestDto registrationDto)
    {
        if (await _userManager.FindByEmailAsync(registrationDto.Email) is not null)
            return new AuthModel { Errors = new List<string> { ErrorMessages.Register.EmailInUse } };

        if (await _userManager.FindByNameAsync(registrationDto.UserName) is not null)
            return new AuthModel { Errors = new List<string> { ErrorMessages.Register.UsernameInUse } };

        var user = _mapper.Map<User>(registrationDto);

        var result = await _userManager.CreateAsync(user, registrationDto.Password);
        if (!result.Succeeded)
            return new AuthModel { Errors = result.Errors.Select(e => e.Description).ToList() };

        var roleResult = await AddRoleAsync(new AddRoleDto { UserId = user.Id, Role = registrationDto.Role });

        var jwtSecurityToken = await GenerateToken(user);
        
        return new AuthModel
        {
            Email = user.Email,
            Username = user.UserName,
            IsAuthenticated = true,
            Roles = new List<string> { string.IsNullOrEmpty(roleResult) ? registrationDto.Role : null! },
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            //ExpiresOn = jwtSecurityToken.ValidTo
        };
    }

    public async Task<AuthModel> LoginAsync(LoginRequestDto loginDto)
    {
        var authModel = new AuthModel();
        var user = new User();
        var email = new EmailAddressAttribute();

        if (email.IsValid(loginDto.Username))
            user = await _userManager.FindByEmailAsync(loginDto.Username);
        else
            user = await _userManager.FindByNameAsync(loginDto.Username);

        if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            authModel.Errors = new List<string> { ErrorMessages.Login.InvalidAuthentication };
            return authModel;
        }

        var jwtSecurityToken = await GenerateToken(user);
        var rolesList = await _userManager.GetRolesAsync(user);

        authModel.IsAuthenticated = true;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        //authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Email = user.Email;
        authModel.Username = user.UserName;
        authModel.Roles = rolesList.ToList();

        return authModel;
    }

    public async Task<string> AddRoleAsync(AddRoleDto roleDto)
    {
        var user = await _userManager.FindByIdAsync(roleDto.UserId);
        if (user is null || !await _roleManager.RoleExistsAsync(roleDto.Role))
            return ErrorMessages.Role.InvalidUserIdOrRolr;

        if (await _userManager.IsInRoleAsync(user, roleDto.Role))
            return ErrorMessages.Role.AlreadyAssigned;

        var result = await _userManager.AddToRoleAsync(user, roleDto.Role);

        return result.Succeeded ? string.Empty : ErrorMessages.Generic.SomethingWentWrong;
    }

    private async Task<JwtSecurityToken> GenerateToken(User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
            roleClaims.Add(new Claim(ClaimTypes.Role, role));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken
        (
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwtConfig.DurationInDays),
            signingCredentials: signingCredentials
        );

        return jwtSecurityToken;
    }


}
