using BuisnnesService.Commands.Notes.Update;
using BuisnnesService.Commands.Tasks.Create;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Task
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x=>x.Title).NotEmpty().MaximumLength(30).WithMessage("Max length 30");
            RuleFor(x => x.Description).NotEmpty();

        }
    }
}
