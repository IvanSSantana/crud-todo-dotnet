using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels;

public class LoginVM
{
    [Display(Name = "E-mail", Prompt = "Digite seu email...")]
    [Required(ErrorMessage = "O e-mail de acesso é obrigatório!")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido!")]
    public string Email { get; set; }

    [Display(Name = "Senha", Prompt = "Digite sua senha...")]
    [Required(ErrorMessage = "A senha de acesso é obrigatório!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }    
    public string ReturnUrl { get; set; }

}
