using CarRental.Authentication.Models.DTOs.Incoming;
using CarRental.Authentication.Services;
using CarRental.Configuration.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.V1.Auth;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequestDto registrationDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(registrationDto);

        if (!result.IsAuthenticated)
            return BadRequest(result);

        SetRefreshTokenInCookie(result.RefreshToken!, result.RefreshTokenExpiration);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);

        if (!result.IsAuthenticated)
            return BadRequest(result);

        if (!string.IsNullOrEmpty(result.RefreshToken))
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

        return Ok(result);
    }

    [HttpGet("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        var result = await _authService.RefreshTokenAsync(refreshToken);

        if (!result.IsAuthenticated)
            return BadRequest(result);

        SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

        return Ok(result);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeToken dto)
    {
        var token = dto.Token ?? Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(token))
            return BadRequest(ErrorMessages.Login.TokenIsRequired);

        var result = await _authService.RevokeTokenAsync(token);

        if (!result)
            return BadRequest(ErrorMessages.Login.InvalidToken);

        return Ok();
    }

    [HttpPost("role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AsignRoleAsync([FromBody] AssignRoleDto roleDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.AssignRoleAsync(roleDto);

        if (!string.IsNullOrEmpty(result))
            return BadRequest(result);

        return Ok(roleDto);
    }

    private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = expires.ToLocalTime(),
        };

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}
