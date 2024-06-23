using Microsoft.AspNetCore.Mvc;
using ContactBook.Application.Services.User;
using ContactBook.Application.DTOs;

namespace ContactBook.Api.Controllers;

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
