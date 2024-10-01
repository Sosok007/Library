﻿using System.ComponentModel.DataAnnotations;

namespace Biblioteka.PL.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Имя пользователя обязательно к заполнению")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Пароль обязателен к заполнению")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}