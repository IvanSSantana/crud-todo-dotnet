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
        if (!ModelState.IsValid) TempData["Failure"] = "Dados inválidos. Verifique os campos preenchidos.";
        
        var result = await _userService.Login(login);

        if (result.Succeeded) TempData["Sucess"] = "Login realizado com sucesso! Redirecionando...";
        else if (result.IsLockedOut) TempData["Failure"] = "Usuário bloqueado por muitas tentativas.";
        else if (result.IsNotAllowed) TempData["Failure"] = "Usuário sem permissão para acessar o sistema.";
        else TempData["Failure"] = "E-mail ou senha incorretos. Tente novamente.";

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
