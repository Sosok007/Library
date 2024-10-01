using System.ComponentModel.DataAnnotations;

namespace Biblioteka.PL.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Имя пользователя обязательно к заполнению")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Пароль обязателен к заполнению")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Подтверждение пароля обязательно")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}