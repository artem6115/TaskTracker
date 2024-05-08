using BuisnnesService.Commands.Epics.Create;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Epic
{
    public class CreateEpicCommandValidator : AbstractValidator<CreateEpicCommand>
    {
        public CreateEpicCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Максимальная длинна заголовка - 20 символов");
            RuleFor(x => x.ProjectId).NotNull();
        }
    }
}
