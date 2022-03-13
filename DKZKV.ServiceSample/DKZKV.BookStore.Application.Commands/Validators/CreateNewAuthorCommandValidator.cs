using DKZKV.BookStore.Application.Commands.Authors;
using FluentValidation;
using JetBrains.Annotations;

namespace DKZKV.BookStore.Application.Commands.Validators;

[UsedImplicitly]
public class CreateNewAuthorCommandValidator : AbstractValidator<CreateNewAuthorCommand>
{
    public CreateNewAuthorCommandValidator()
    {
        RuleFor(o => o.FirstName)
            .NotNull()
            .MaximumLength(120)
            .NotEmpty().WithMessage("Author first name can not be empty, and greater than 120 characters");

        RuleFor(o => o.LastName)
            .NotNull()
            .MaximumLength(120)
            .NotEmpty().WithMessage("Author last name can not be empty, and greater than 120 characters");

        RuleFor(o => o.BirthDate)
            .NotEqual(DateOnly.MinValue)
            .NotEqual(DateOnly.MaxValue)
            .WithMessage("Author birth date is invalid");

        RuleFor(o => o.FirstName).NotEmpty().WithMessage("Author name can not be empty");
    }
}