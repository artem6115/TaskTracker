using BuisnnesService.Commands.Projects.Update;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Project
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Имя проекта должно быть заполнено");
            RuleFor(x => x.AuthorId).NotNull().WithMessage("AuthorId is required");
        }
    }
}
