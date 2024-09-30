using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Biblioteka.API.Models;
using Biblioteka.BLL;
using Biblioteka.DAL;
using Biblioteka.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Biblioteka.API.Controllers;

[ApiController]
[Route("[controller]")]
public partial class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public UserController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }
    [HttpPost("reg")]
    public async Task<IActionResult> Register(LoginModel request)
    {
        var res = await _userService.AddUserAsync(new Polzak
        {
            Username = request.Username,
            Password = GetHash(request.Password),
        });
        return Ok(res); 
    }
    [HttpPost("auth")]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var user = await _userService.GetUserByCredentialsAsync(loginModel.Username, GetHash(loginModel.Password));

        if (user == null)
        {
            return NotFound();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)), SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(tokenString);
    }
    private string GetHash(string password)
    {
        using var sha = SHA512.Create();
        var sb = new StringBuilder();
        foreach (var item in sha.ComputeHash(Encoding.Unicode.GetBytes(password)))
        {
            sb.Append(item.ToString("X2"));
        }

        return sb.ToString();
    }
}