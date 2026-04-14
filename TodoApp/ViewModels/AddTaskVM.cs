using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels;

public class AddTaskVM
{
    [Required(ErrorMessage = "O título é obrigatório")]
    [Display(Name = "Título da Tarefa", Prompt = "Digite o título da tarefa")]
    [StringLength(100)]
    public string Title { get; set; }

    [Display(Name = "Descrição", Prompt = "Descreva os detalhes da tarefa")]
    public string Description { get; set; }
}