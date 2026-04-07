using TodoApp.Models;

namespace TodoApp.ViewModels;

public class HomeVM
{
    public int TotalTask { get; set; }
    public int OpenTasks { get; set; }
    public int EndedTasks { get; set; }
    public List<ToDo> ToDos { get; set; }
}