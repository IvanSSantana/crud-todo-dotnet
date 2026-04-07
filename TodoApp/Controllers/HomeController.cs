using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Models;
using TodoApp.Data;
using TodoApp.Services;
using Microsoft.EntityFrameworkCore;
using TodoApp.ViewModels;

namespace TodoApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IUserService _userService;

    public HomeController(AppDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userService.GetLoggedUser();

        var openTodos = await _dbContext.ToDos
            .Where(todo => todo.UserId == user.Id && !todo.Done)
            .OrderByDescending(todo => todo.CreatedAt)
            .ThenBy(todo => todo.Title)
            .ToListAsync();

        var totalTasks = await _dbContext.ToDos.CountAsync(todo => todo.UserId == user.Id);

        var EndedTasks = await _dbContext.ToDos.CountAsync(t => t.UserId == user.Id && t.Done);

        HomeVM homeVM = new()
        {
            TotalTask = totalTasks,
            OpenTasks = openTodos.Count,
            EndedTasks = EndedTasks,
            ToDos = openTodos
        };
        return View(homeVM);
    }

    [HttpPost]
    public async Task<IActionResult> EndTask (int? id)
    {
        if (id == null)
        {
            return Json(new
            {
                sucess = false,
                message = "Problemas ao carregar a tarefa! Tente novamente mais tarde..."
            });
        }

        var todo = await _dbContext.ToDos.Where(todo => todo.Id == id).SingleOrDefaultAsync();

        var user = await _userService.GetLoggedUser();
        if (user == null)
        {
            return Json(new
            {
                success= false,
                message = "Sua sessão expirou, faça login para continuar!",
                redirect = true
            });
        }

        if (todo.UserId != user.Id)
        {
            return Json(new
            {
                success = false,
                message = "Você não tem permissão para alterar a tarefa!"
            });
        }

        try
        {
            todo.Done = true;
            _dbContext.ToDos.Update(todo);
            _dbContext.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Tarefa finalizada com sucesso! Recarregue a lista..."
            });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                success = false,
                message = "Ocorreu um problema ao finalizar a tarefa. Tente novamente mais tarde...",
                details = ex.Message
            });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
