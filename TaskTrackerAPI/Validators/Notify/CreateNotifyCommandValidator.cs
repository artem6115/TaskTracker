using BuisnnesService.Commands.Notifies.Create;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Notify
{
    public class CreateNotifyCommandValidator : AbstractValidator<CreateNotifyCommand>
    {
        public CreateNotifyCommandValidator()
        {
            RuleFor(x=>x.Message).NotEmpty();
            RuleFor(x => x.UserId).NotNull();

        }
    }
}
