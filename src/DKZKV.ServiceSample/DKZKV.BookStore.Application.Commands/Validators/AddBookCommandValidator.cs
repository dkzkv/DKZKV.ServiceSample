using DKZKV.BookStore.Application.Commands.Books;
using FluentValidation;
using JetBrains.Annotations;

namespace DKZKV.BookStore.Application.Commands.Validators;

[UsedImplicitly]
public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator()
    {
        RuleFor(o => o.BookName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(250)
            .WithMessage("Book name should be non empty and less than 250 characters");

        RuleFor(o => o.WroteAt)
            .NotEqual(DateOnly.MinValue)
            .NotEqual(DateOnly.MaxValue)
            .WithMessage("Book wrote date is invalid");
    }
}