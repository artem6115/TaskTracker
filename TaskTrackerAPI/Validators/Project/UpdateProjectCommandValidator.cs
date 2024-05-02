using BuisnnesService.Commands.Projects.Update;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Project
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id is required");
            RuleFor(x=>x.Name).NotEmpty().MaximumLength(20).WithMessage("Максимальная длинна название - 20");
            RuleFor(x => x.AuthorId).NotNull().WithMessage("AuthorId is required");
        }
    }
}
