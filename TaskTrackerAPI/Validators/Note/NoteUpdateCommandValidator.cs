using BuisnnesService.Commands.Notes.Update;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Note
{
    public class NoteUpdateCommandValidator : AbstractValidator<NoteUpdateCommand>
    {
        public NoteUpdateCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is requires");
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id is requires");
        }
    }
}
