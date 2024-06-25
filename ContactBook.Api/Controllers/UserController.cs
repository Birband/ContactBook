using Microsoft.AspNetCore.Mvc;
using ContactBook.Application.DTOs;
using ContactBook.Application.Services.Users;

namespace ContactBook.Api.Controllers;

/// <summary>
/// Controller for managing users 
/// </summary>
/// <response code="401">If the user is not authenticated</response>
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        var response = await _userService.LoginAsync(request);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
    {
        var response = await _userService.RegisterAsync(request);
        return Ok(response);
    }    
}
