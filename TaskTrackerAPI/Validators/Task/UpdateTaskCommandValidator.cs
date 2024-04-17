using BuisnnesService.Commands.Tasks.Create;
using BuisnnesService.Commands.Tasks.Update;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Task
{
    public class UpdateTaskCommandValidator: AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(30).WithMessage("Max length 30");
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
