﻿using BuisnnesService.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace TaskTrackerAPI.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator() {

            RuleFor(x => x.Id).Empty();
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(30);
            RuleFor(x=>x.Email).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Почта задана не корректно");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).WithMessage("Минимальная длинна пароля 5 символов")
                .Must(PasswordValidator)
                .WithMessage("Пароль должен иметь хотя бы одну букву в верхнем и нижнем регистре");
        }
        public static bool PasswordValidator(string password)
            => (password != password.ToLower() && password != password.ToUpper());
    }
}
