using BuisnnesService.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace TaskTrackerAPI.Validators.User
{
    public class UserRegistDtoValidator : AbstractValidator<UserRegistDto>
    {
        public UserRegistDtoValidator()
        {

            RuleFor(x => x.FullName).NotEmpty().WithMessage("ФИО обязательное поле")
                .MaximumLength(50).WithMessage("Минимальная длинна ФИО 50 символов")
                .Must(x => x.Split(' ').Length >= 3).WithMessage("ФИО не полное");
            RuleFor(x => x.Email).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Почта задана не корректно");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).WithMessage("Минимальная длинна пароля 5 символов")
                .Must(UserLoginDtoValidator.PasswordValidator)
                .WithMessage("Пароль должен иметь хотя бы одну букву в верхнем и нижнем регистре");
        }
    }
}
