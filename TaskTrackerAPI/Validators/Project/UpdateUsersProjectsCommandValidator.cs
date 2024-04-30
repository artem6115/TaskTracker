using BuisnnesService.Commands.Projects.Update;
using BuisnnesService.Commands.Projects.UpdateUsers;
using FluentValidation;

namespace TaskTrackerAPI.Validators.Project
{
    public class UpdateUsersProjectsCommandValidator : AbstractValidator<UpdateUsersProjectsCommand>
    {
        public UpdateUsersProjectsCommandValidator()
        {
            RuleFor(x=>x.ProjectId).NotNull();
        }
    }
}
