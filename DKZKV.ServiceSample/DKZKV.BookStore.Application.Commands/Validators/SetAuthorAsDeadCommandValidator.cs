using DKZKV.BookStore.Application.Commands.Authors;
using FluentValidation;
using JetBrains.Annotations;

namespace DKZKV.BookStore.Application.Commands.Validators;

[UsedImplicitly]
public class SetAuthorAsDeadCommandValidator : AbstractValidator<SetAuthorAsDeadCommand>
{
    public SetAuthorAsDeadCommandValidator()
    {
        RuleFor(o => o.DeathDate)
            .NotEqual(DateOnly.MinValue)
            .NotEqual(DateOnly.MaxValue)
            .WithMessage("Author death date is invalid");
    }
}