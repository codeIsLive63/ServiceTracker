using System.ComponentModel.DataAnnotations;

namespace ServiceTracker.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Это поле обязательно для заполнения")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Логин должен быть не менее 4 символов")]
    [Display(Name = "Логин")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Это поле обязательно для заполнения")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Пароль должен быть не менее 4 символов")]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Запомнить меня?")]
    public bool RememberMe { get; set; }

    public string ReturnUrl { get; set; }
}
