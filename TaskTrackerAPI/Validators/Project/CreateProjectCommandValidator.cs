using BuisnnesService.Commands.Projects.Create;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Project
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Имя проекта должно быть заполнено");
            RuleFor(x => x.AuthorId).NotNull().WithMessage("AuthorId is required");

        }
    }
}
