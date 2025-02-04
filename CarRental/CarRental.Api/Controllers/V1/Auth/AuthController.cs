using AutoMapper;
using CarRental.Authentication.Models.DTOs.Incoming;
using CarRental.Authentication.Services;
using CarRental.Entities.DbSets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
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

        if(!result.IsAuthenticated) 
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequestDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);

        if (!result.IsAuthenticated)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("role")]
    public async Task<IActionResult> AddRoleAsync([FromBody]AddRoleDto roleDto)
    {
        //if (!ModelState.IsValid)
        //    return BadRequest(ModelState);

        var result = await _authService.AddRoleAsync(roleDto);

        if(!string.IsNullOrEmpty(result))
            return BadRequest(result);

        return Ok(roleDto);
    }
}
