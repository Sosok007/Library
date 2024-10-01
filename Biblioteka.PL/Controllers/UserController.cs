using System.Text;
using System.Text.Json;
using Biblioteka.BLL;
using Biblioteka.Entities;
using Biblioteka.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.PL.Controllers;

public class UserController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IUserService _userService;

    public UserController(HttpClient httpClient, IUserService userService)
    {
        _httpClient = httpClient;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var json = JsonSerializer.Serialize(model);
        var request = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("http://localhost:5194/User/auth", request);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            
            
            Response.Cookies.Append("JwtToken", responseData, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });

            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
        return View(model);
    }
    [HttpGet]
    public ActionResult AddUser()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddUser(RegistorModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var polzak = new Polzak
        {
            Username = model.Username,
            Password = model.Password
        };

        try
        {
            await _userService.AddUserAsync(polzak);
            return RedirectToAction("Login");
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
        }
        return View(model);
    }
}