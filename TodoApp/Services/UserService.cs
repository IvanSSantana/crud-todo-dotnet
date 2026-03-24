using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TodoApp.Data;
using TodoApp.Helpers;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Services;

public class UserService : IUserService
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<UserService> _logger;
    private readonly AppDbContext dbContext;
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserService(
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        ILogger<UserService> logger,
        AppDbContext dbContext,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        this.dbContext = dbContext;
        this.httpContextAccessor = httpContextAccessor;
    }

    public Task<UserVM> GetLoggedUser()
    {
        throw new NotImplementedException();
    }

    public async Task<SignInResult> Login(LoginVM login)
    {
        string userName = login.Email;

        if (Helper.IsValidEmail(login.Email))
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user != null) userName = user.UserName;
        }

        var result = await _signInManager.PasswordSignInAsync(userName, login.Password, login.RememberMe, lockoutOnFailure: true);
        
        if (result.Succeeded) _logger.LogInformation($"Usuário '{userName}' acessou o sistema.");
        if (result.IsLockedOut) _logger.LogWarning($"Usuário '{userName}' está bloqueado.");
        if (result.IsNotAllowed) _logger.LogWarning($"Usuário '{userName}' está tentando acessar uma área restrita.");

        return result;
    }

    public async Task Logout()
    {
        _logger.LogInformation($"Usuário '{ClaimTypes.Email}' saiu do sistema.");
        await _signInManager.SignOutAsync();
    }
}
