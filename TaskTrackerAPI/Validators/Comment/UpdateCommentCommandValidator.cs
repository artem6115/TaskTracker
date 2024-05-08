using BuisnnesService.Commands.Comment.Update;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Comment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Комментарий не должен быть пустым");
        }
    }
}
