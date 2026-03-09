using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login()
    {
        return View();
    }
    
    public IActionResult Logout()
    {
        return RedirectToAction("Login");
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Profile()
    {
        return View();
    }
}
