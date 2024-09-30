using System.Text;
using System.Text.Json;
using Biblioteka.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.PL.Controllers;

public class UserController : Controller
{
    private readonly HttpClient _httpClient;

    public UserController(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
}