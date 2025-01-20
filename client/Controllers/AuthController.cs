using Microsoft.AspNetCore.Mvc;

namespace client.Controllers;

public class AuthController : Controller
{
    public AuthController()
    {
        
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string? email, string? password)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
}