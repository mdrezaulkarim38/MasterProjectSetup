using api.Dtos.Auth;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public AuthController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new AppUser
        {
            UserName = model.Email,
            Email = model.Email,
            Name = model.Username,
        };
        var result = await _userManager.CreateAsync(user, model.Password!);
        if (result.Succeeded)
        {
            return Ok(new { Message = "User registered successfully" });
        }
        foreach (var error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);
        
        return BadRequest(ModelState);
    }
}