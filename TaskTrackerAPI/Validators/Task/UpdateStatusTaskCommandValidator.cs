using BuisnnesService.Commands.Tasks.Update;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Task
{
    public class UpdateStatusTaskCommandValidator : AbstractValidator<UpdateStatusTaskCommand>
    {
        public UpdateStatusTaskCommandValidator() 
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.StatusTask).NotEmpty().IsInEnum().WithMessage("Status is incorrected");
        }
    }
}
