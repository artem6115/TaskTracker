﻿using BuisnnesService.Commands.Notes.Create;
using FluentValidation;

namespace TaskTrackerAPI.Validators
{
    public class NoteCreateCommandValidator : AbstractValidator<NoteCreateCommand>
    {
        public NoteCreateCommandValidator()
        {
            RuleFor(x=>x.Description).NotEmpty().WithMessage("Description is requires");
        }
    }
}
