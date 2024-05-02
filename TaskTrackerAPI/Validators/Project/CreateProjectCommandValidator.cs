using BuisnnesService.Commands.Projects.Create;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Project
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().MaximumLength(20).WithMessage("Максимальная длинна название - 20");
            RuleFor(x => x.AuthorId).NotNull().WithMessage("AuthorId is required");

        }
    }
}
