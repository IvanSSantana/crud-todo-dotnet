using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using TodoApp.ViewModels;

namespace TodoApp.Services;

public interface IUserService
{
    Task<UserVM> GetLoggedUser();
    Task<SignInResult> Login(LoginVM login);
    Task Logout();
}
