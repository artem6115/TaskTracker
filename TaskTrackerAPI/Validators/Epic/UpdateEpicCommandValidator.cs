using BuisnnesService.Commands.Epics.Update;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Epic
{
    public class UpdateEpicCommandValidator : AbstractValidator<UpdateEpicCommand>
    {
        public UpdateEpicCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Максимальная длинна заголовка - 20 символов");
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Id).NotNull();
        }
    }
}
