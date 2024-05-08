using BuisnnesService.Commands.Comment.Create;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Comment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.WorkTaskId).NotNull();
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Комментарий не должен быть пустым");
        }
    }
}
