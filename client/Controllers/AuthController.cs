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
}