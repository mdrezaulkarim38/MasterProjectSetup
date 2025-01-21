using System.Net.Http;
using System.Text;
using System.Text.Json;
using client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace client.Controllers;

public class AuthController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    public AuthController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(User model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var client = new HttpClient(); // Create HttpClient
        var loginPayload = new { model.Email, model.Password };
        var jsonContent = new StringContent(JsonSerializer.Serialize(loginPayload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:5035/api/Auth/login", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync(); // Read response as string
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseBody); // Deserialize into TokenResponse
        
            if (!string.IsNullOrEmpty(tokenResponse?.Token))
            {
                // Save token in a cookie
                HttpContext.Response.Cookies.Append("AuthToken", tokenResponse.Token);

                // Redirect to the home page
                return RedirectToAction("Index", "Home");
            }
        }

        // If login fails
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        return RedirectToAction("Login", "Auth");
    }
}