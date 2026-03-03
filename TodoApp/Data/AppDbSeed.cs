using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TodoApp.Models;

namespace TodoApp.Data;

public class AppDbSeed
{
    public AppDbSeed(ModelBuilder builder)
    {
        #region Popular dados de Perfil de Usuário
        List<IdentityRole> roles = new ()
        {
            new IdentityRole()
            {
                Id = "0748e4ff-eae3-479e-af58-fa4f8738d7a8",
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole()
            {
                Id = "fb01240f-55bb-40f2-a0d4-2d1b7bcc9196",
                Name = "Usuário",
                NormalizedName = "USUÁRIO"
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
        #endregion

        #region  Popular dados de Usuário
        List<AppUser> users = new()
        {
            new AppUser()
            {
                Id = "803eace2-88ac-444f-9602-0123749f8160",
                Email = "escolaautosave@gmail.com",
                NormalizedEmail = "ESCOLAAUTOSAVE@GMAIL.COM",
                UserName = "escolaautosave@gmail.com",
                LockoutEnabled = false,
                EmailConfirmed = true,
                Name = "Ivan Santos Santana",
                ProfilePicture = "/img/users/803eace2-88ac-444f-9602-0123749f8160.png"
            }, 
            new AppUser()
            {
                Id = "b9a4a834-1df4-4b75-9ee2-038c2c0f4071",
                Email = "reisfelipe546@gmail.com",
                NormalizedEmail = "REISFELIPE546@GMAIL.COM",
                UserName = "reisfelipe546@gmail.com",
                LockoutEnabled = false,
                EmailConfirmed = true,
                Name = "Felipe Henrique Da Paixão Reis",
                ProfilePicture = "/img/users/b9a4a834-1df4-4b75-9ee2-038c2c0f4071.png"
            }, 
        };
        foreach (var user in users)
        {
            PasswordHasher<IdentityUser> pass = new();
            user.PasswordHash = pass.HashPassword(user, "123456");
        }
        builder.Entity<AppUser>().HasData(users);
        #endregion

        #region Popular dados de Usuário Perfil
        List<IdentityUserRole<string>> userRoles = new()
        {
            new IdentityUserRole<string>()
            {
                UserId = users[0].Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>()
            {
                UserId = users[1].Id,
                RoleId = roles[1].Id
            },
        };
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion

        #region  Popular as Tarefas do usuário 
        List<ToDo> toDos = new()
        {
            new ToDo()
            {
                Id = 1,
                Title = "Terminar Homepage Figma",
                Description = "Finalizar até hoje",
                UserId = users[0].Id
            },
            new ToDo()
            {
                Id = 2,
                Title = "Levar minha vó no judô",
                Description = "Até 17:00",
                UserId = users[1].Id,
            },
        };
        #endregion
    }    
}
