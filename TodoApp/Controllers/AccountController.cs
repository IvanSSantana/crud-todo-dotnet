using Microsoft.AspNetCore.Mvc;
using TodoApp.Services;
using TodoApp.ViewModels;

namespace TodoApp.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IUserService _userService;

    public AccountController(ILogger<AccountController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

        var model = new LoginVM
        {
            ReturnUrl = returnUrl ?? Url.Content("~/") 
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(LoginVM login)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.Login(login);

            if (result.Succeeded) return LocalRedirect(login.ReturnUrl);
            if (result.IsLockedOut) return RedirectToAction("Lockout");
            if (result.IsNotAllowed) return RedirectToAction("AcessDenied");

            ModelState.AddModelError("", "Usuário e/ou senha inválidos.");
        } 
        
        return View(login);
        
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
