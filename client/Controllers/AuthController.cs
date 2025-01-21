using client.Models;
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
    public IActionResult Login(User model)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
}